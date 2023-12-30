using System.Reflection;
using Application.CQRS.Handler;
using Application.Interface;
using Domain.Data.DbContexts;
//using Domain.Data.DbContexts;
using Infrastructure.Service.ConfigurationPage;
using Infrastructure.Service.ExportPage;
using Infrastructure.Service.ParameterMappingPage;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Application;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ExportDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




builder.Services
        .AddInfrastructure(builder.Configuration) 
        .AddApplication(builder.Configuration);

// builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
// builder.Services.AddMediatR(typeof(GetAllDropdownDataQueryHandler).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
      app.UseCors(options =>
               options.WithOrigins("*")
               .AllowAnyMethod()
               .AllowAnyHeader()
               );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(options =>
               options.WithOrigins("*")
               .AllowAnyMethod()
               .AllowAnyHeader()
          
               );
app.MapControllers();

app.Run();
