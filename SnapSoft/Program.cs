using Microsoft.EntityFrameworkCore;
using SnapSoft.DataHandler;
using SnapSoft.Middlewares;
using SnapSoft.Services;

var allowSpecificOrigin = "_allowSpecificOrigin_";
var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SnapSoftDataContext>(
        o => o.UseNpgsql(builder.Configuration.GetConnectionString("PostgreDb"))
);

builder.Services.AddScoped<ICalculateService, CalculateService>();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowSpecificOrigin,
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(allowSpecificOrigin);

app.UseAuthorization();

app.UseMiddleware<HttpLogMiddleware>();

app.MapControllers();

app.Run();
