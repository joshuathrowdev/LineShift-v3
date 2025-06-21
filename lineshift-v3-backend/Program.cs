
using lineshift_v3_backend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using lineshift_v3_backend.Services;
using lineshift_v3_backend.DataAccess.Repository;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lineshift_v3_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Any dependencies or transative dependencies to controller (services and repos)
            builder.Services.AddSingleton<ISportsService, SportsService>();


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

            // Add other services (Authentication, Authorization, etc.)
            // ...

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // --- MIDDLEWARE PIPELINE (app.Use...) ---
            // This is where request processing middleware is ordered.

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
