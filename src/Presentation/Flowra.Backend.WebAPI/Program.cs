using Flowra.Backend.Application;
using Flowra.Backend.Application.Interfaces.Repositories;
using Flowra.Backend.Application.Interfaces.Services;
using Flowra.Backend.Application.Services;
using Flowra.Backend.Infrastructure;
using Flowra.Backend.Persistence;
using Flowra.Backend.Persistence.Repositories;
using Flowra.Backend.WebAPI;
using Flowra.Backend.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddPresentationServices(builder.Configuration);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200", "http://localhost:4280")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddSwaggerGen();


var app = builder.Build();


//
// MIDDLEWARE PIPELINE
//

// CorrelationId üretimi (tüm request boyunca kullanýlacak)
app.UseMiddleware<CorrelationIdMiddleware>();
// HTTPS redirect
app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");
app.MapControllers();

app.Run();
