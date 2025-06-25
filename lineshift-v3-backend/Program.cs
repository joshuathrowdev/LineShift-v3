
using lineshift_v3_backend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using lineshift_v3_backend.Services;
using lineshift_v3_backend.DataAccess.Repository;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.CompilerServices;
using Serilog;

namespace lineshift_v3_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Any dependencies or transative dependencies to controller (services and repos)
            builder.Services.AddScoped<ISportsService, SportsService>();
            builder.Services.AddScoped<ISportsRepository,  SportsRepository>();


            // Add the DBContext (Connection/Connection String) to MariaDB

                // 1. Get the connection String from appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("MariaDbConnection")
                ?? throw new InvalidOperationException("Connection string 'MariaDbConnection' not found ");

                // 2. Configure our DbContext for MariaDB
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                // 3. Specify the server version of our MariaDB instance
                // we could explicity state the server version if we know (better for prod env)
                // EX: var serverVersion = new MariaDbServerVersion(new Version(10, 11, 2))
                var serverVersion = new MariaDbServerVersion(new Version(11, 8, 2));
                options.UseMySql(connectionString, serverVersion, mySqlOptions =>
                {
                    mySqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                     );

                    // Optional: Configure specific data type mappings
                    // This is useful if you want to explicitly control how certain C# types map to MariaDB types.
                    // For example, mapping C# bool to MariaDB TINYINT(1) (default for Pomelo) or BIT(1)
                    // mySqlOptions.DefaultDataTypeMappings(mappings => mappings.WithClrBoolean(MySqlBooleanType.Bit1));

                    // Optional: Configure behavior for schema (MySQL/MariaDB don't have EF Core schemas)
                    // mySqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore); // Or .Throw()
                });
            });

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
            // ...

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // Register and config the swagger generator with the builder (server container) so it can
            // inspect our api endpoints and build out the UI
            builder.Services.AddSwaggerGen(options =>
            {
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


            // Automatically Applying Migrations on applcation start up (In Dev Env)
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                // everytime the application is ran locally (through VSCode) or deployed to a server (on application startup)
                // we will create a new scipe of the app services and pull the DbContext related to our connection 
                // then, the .Database.Migrate() will automatically compare out migrations folder to the databases histoty of migration
                // and applly the local migration folder changes in order (if any)
                // then the scope is resolved and the app is ran normally
            }

                app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
