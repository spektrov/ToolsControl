using GemBox.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using ToolsControl.BLL.AutoMapper;
using ToolsControl.BLL.Interfaces;
using ToolsControl.BLL.Utilities;
using ToolsControl.DAL;
using ToolsControl.DAL.Interfaces;
using ToolsControl.WebAPI.AutoMapper;
using ToolsControl.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(typeof(MappingProfileBusiness), typeof(MappingProfileApi));

builder.Services.AddDbContext<ToolsDbContext>(opts => 
        opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

builder.Services.AddRepositories();
builder.Services.AddAddBusinessServices();
builder.Services.AddValidationFilters();

builder.Services.ConfigureTokens();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureCors();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureSwagger();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddMemoryCache();

builder.Services.AddScoped<IEquipmentReportGenerator<ExcelFile>, ExcelEquipmentReportGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();

app.UseResponseCaching();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();