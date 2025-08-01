
using lineshift_v3_backend.DataAccess.Repository;
using lineshift_v3_backend.DataAccess.Repository.Identity;
using lineshift_v3_backend.Infrastructure;
using lineshift_v3_backend.MappingProfiles;
using lineshift_v3_backend.Models;
using lineshift_v3_backend.Services;
using lineshift_v3_backend.Services.Identity;
using lineshift_v3_backend.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using AutoMapper;
using System.Reflection;

namespace lineshift_v3_backend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // --- Services and DbContext ---

            // Add services to the container.
            // Any dependencies or transitive dependencies to controller (services and repos)
            builder.Services.AddScoped<ITokenServices, TokenServices>();
            builder.Services.AddScoped<IAuthServices, AuthServices>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();

            builder.Services.AddScoped<ISportsService, SportsService>();
            builder.Services.AddScoped<ISportsRepository,  SportsRepository>();

            builder.Services.AddScoped<IGoverningBodiesServices, GoverningBodiesServices>();
            builder.Services.AddScoped<IGoverningBodiesRepository, GoverningBodiesRepository>();

            builder.Services.AddScoped<ILeaguesServices, LeaguesServices>();
            builder.Services.AddScoped<ILeaguesRepository, LeaguesRepository>();


            // Configuring Auto Mapper for UserProfile
            // Source: ApplicationUser, Dest: SessionUser
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserProfil>();
                cfg.AddProfile<SportProfile>();
                cfg.AddProfile<GoverningBodyProfile>();
                cfg.AddProfile<LeagueProfile>();
            });


            // Add the DBContext (Connection/Connection String) to MariaDB
            // 1. Get the connection String from appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("MariaDbConnection")
                ?? throw new InvalidOperationException("Connection string 'MariaDbConnection' not found ");

                // 2. Configure our DbContext for MariaDB
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                // 3. Specify the server version of our MariaDB instance
                // we could explicity state the server version if we know (better for prod env)
                var serverVersion = new MariaDbServerVersion(new Version(11, 8, 2));
                options.UseMySql(connectionString, serverVersion, mySqlOptions =>
                {
                    mySqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                     );
                });
            });

            
            // --- Logging and Debugging ---

            // Add other services (Authentication, Authorization, logger, etc.)
            builder.Host.UseSerilog((context, loggerConfig) =>
            {
                loggerConfig.ReadFrom.Configuration(context.Configuration) // Read from appsettings.json
                    .Enrich.FromLogContext() // Add contextual properties (e.g., correlation IDs)
                    .WriteTo.Console() // Still log to console for local dev feedback
                    .WriteTo.File("logs/log-txt", // Log to a file in the backend root "logs" folder
                                  rollingInterval: RollingInterval.Day, // New File Daily
                                  outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}");
            });



            // --- Authentication and Authorization with Identity EFCore Framework ---

            // Identity (through Identity.EFCore for authentication and authorization) Services 
            // Registers the two default services: UserManager and SignInManager and cast them
            // to the ApplicationUser Model
            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                // Email Verification for traffic/account registering integrity and control
                // If this was a prod level application, we would NEED some way to verify users and that would
                // go here
                // The value would need to be set to: true
                options.SignIn.RequireConfirmedEmail = false;

                // We would also define other configuration setting such as 
                // - password requirements
                // - lockout settings
                // - user settings
                // - sign in settings
            })
                .AddRoles<IdentityRole>() // registers the role portion of user management with Identity Framework
                                          // Database Integration Part
                                          // Tells ASP.NEXT Core to use EFCore and our applicationDbContext as the persistence mechanism for
                                          // storing all Identity-related data
                                          // Go through out applicationDbContext to make these tables and all related meta data
                .AddEntityFrameworkStores<ApplicationDbContext>();


            builder.Services.AddAuthentication(options => // registers authentication services with DI container
            {
                // Sets the default scheme to use when authenticating request (EX: checking if a user is logged in)
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // Sets the default scheme to use when an unauthorized request is made 
                // (EX: to redirect to a login page or return a 401 Unauthorized)
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => // Configs the JWT Bearer authentication handler
                {
                    // Defines the parameters the server uses to validate incoming JWT
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Ensures the token's 'iss' (issuer) claim matches a valid issuer
                        ValidateIssuer = true,
                        // Ensures the token's 'aud' (audience) claim matches a valid audience for this API
                        ValidateAudience = true,
                        // Ensures the token's 'exp' (expiration) and 'nbf' (not before) claims are valid (token is not expired or used too early)
                        ValidateLifetime = true,
                        // Ensures the token's signature is valid, preventing tampering
                        ValidateIssuerSigningKey = true,
                        // Specifies the valid issuer(s) for tokens (read from appsettings.json)
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        // Specifies the valid audience(s) for tokens (read from appsettings.json)
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        // Provides the secret key used to verify the token's signature (read from appsettings.json and converted to bytes)
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]
                            ?? throw new InvalidOperationException("JWT Key not found")))
                    };
                });



            // Configuring Global Route Options
            builder.Services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                // Also have the option to make query strings lower case
                //options.LowercaseQueryStrings = true;
            });


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Register and config the swagger generator with the builder (server container) so it can
            // inspect our api endpoints and build out the UI
            builder.Services.AddSwaggerGen(options =>
            {
                // SwaggerGen Security Services Schema
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // authentication schema
                    BearerFormat = "JWT", // format of bearer token
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token in the text input below." +
                    "Example: 'eyJhbGciOiJIUzI1Ni...' (just the token, no 'Bearer ' prefix)"
                });

                // Defining which endpoint require "Bearer" schema
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                options.SwaggerDoc("v3", new OpenApiInfo
                {
                    Version = "3.0",
                    Title = "LineShift-v3 Backend API",
                    Description = "A ASP.NET Core Web API for the lineshift_v3_backend",
                });
            });


            var app = builder.Build();

            // --- MIDDLEWARE PIPELINE (app.Use...) ---
            // This is where request processing middleware is ordered.

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); // Enables the middleware to serve the generated OpenAPI JSON
                // enable swagger UI and serve JSON using the Swagger generated defined by build
                app.UseSwaggerUI(options =>
                {
                    // Specifies the endpoint where the Swagger JSON is served.
                    // The "v3" here must match the document name used in AddSwaggerGen (options.SwaggerDoc("v3", ...)
                    options.SwaggerEndpoint("/swagger/v3/swagger.json", "LineShift-v3 Backend API 3.0");

                    // Optional: Set the Swagger UI to be at the root of your application (e.g., http://localhost:5000/)
                    // instead of http://localhost:5000/swagger
                    // options.RoutePrefix = string.Empty;
                });
            }


            // Automatically Applying Migrations on application start up (In Dev Env)
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                // every time the application is ran locally (through VSCode) or deployed to a server (on application startup)
                // we will create a new scope of the app services and pull the DbContext related to our connection 
                // then, the .Database.Migrate() will automatically compare out migrations folder to the databases history of migration
                // and apply the local migration folder changes in order (if any)
                // then the scope is resolved and the app is ran normally

                // IMPORTANT MIGRATION REMINDER:
                // ---------------------------------------------------------------------------------------------------------
                // When you make changes to your ApplicationDbContext or any entity models (ApplicationUser, Product, etc.):
                // 1. CREATE A NEW MIGRATION:
                //    Open Terminal/PMC in your project directory and run:
                //    dotnet ef migrations add YourMigrationNameHere
                //    (e.g., dotnet ef migrations add AddFirstNameToUsers)
                //
                // 2. APPLY MIGRATIONS:
                //    a) During local development, this code (dbContext.Database.Migrate()) will apply pending migrations
                //       on app startup.
                //    b) FOR PRODUCTION DEPLOYMENTS: It's best practice to apply migrations as a separate step
                //       BEFORE starting your application. Use the command:
                //       dotnet ef database update --project YourProjectName.csproj
                // ---------------------------------------------------------------------------------------------------------
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // handles authentication the user based on credentials
                                     // like cookies, JWT Tokens, API Keys, etc
                                     // Establishes the ClaimsPrinciple for the current request
                                     // Must be called to actually perform the JWT Auth checked described above
            
            app.UseAuthorization(); // uses the authenticated user's identity to determine if they
                                    // are authorized to access a resource
                                    // must be called to perform authorization checks based on authenticated user


            app.MapControllers();

            // Calling DbInitializer to seed the database idempotendly 
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                // Resolve logger for DbInitializer to ensure messages are captured
                var logger = services.GetRequiredService<ILogger<DbInitializerLoggerCategory>>();
                // Call static method to initialize
                await DbInitializer.Initialize(services, logger);
            }

            app.Run();
        }
    }
}
