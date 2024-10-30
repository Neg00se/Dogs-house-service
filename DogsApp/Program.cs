using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using DogsApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DogsAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddAutoMapper(typeof(AutomapperProfile));

builder.Services.AddScoped<IDogsRepository, DogsRepository>();
builder.Services.AddScoped<IDogsService, DogsService>();

builder.Services.AddExceptionHandler<DbExceptionHandler>();
builder.Services.AddExceptionHandler<ArgumentOutOfRangeExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(opts => { });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
