using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToolsControl.DAL;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;
using ToolsControl.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ToolsDbContext>(opts => 
        opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

builder.Services.AddRepositories();

builder.Services.ConfigureTokens();
builder.Services.ConfigureIdentity();



builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();