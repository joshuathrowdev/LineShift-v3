
namespace lineshift_v3_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Registers the swagger generator with the dependency injection container
            // option (parameter) -> option.SwaggerDock() makes a swagger documentation for your api


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Middleware serves the generated Swagger JSON Doc (available if you defined one)
                app.UseSwagger();
                // Middleware serves the interactive swagger UI
                // options (paramter) -> tells the UI where to find the Swagger JSON Doc to render
                // options.RoutePrefix =  string.Empty (optional) makes the Swagger UI accessible
                // directly at the root URL of the applicatoin 
                // Example: https://localhost:port/ instead of https://localhost:port/swagger
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "LineShift v3 Api");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
