using MIRSAL.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var services = builder.Services;

// Add services to the container.
services.AddControllers();
services.AddSingleton<CustomerService>();
services.AddSingleton<RequestService>();
services.AddSingleton<PolicyService>();
services.AddSingleton<VehicleService>();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAllOrigins");

// Add configuration to access MongoDB

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
