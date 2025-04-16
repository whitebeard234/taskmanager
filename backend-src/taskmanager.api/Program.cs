using Microsoft.EntityFrameworkCore;
using taskmanager.api.Data;
using taskmanager.api.Data.Repositories;
using FluentValidation;
using taskmanager.api.Validator;
using taskmanager.api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskItemDtoValidator>();
builder.Services.AddScoped<IValidator<CreateTaskItemDto>, CreateTaskItemDtoValidator>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskManagerDbContext>(options =>
{
    options.UseInMemoryDatabase("TaskManagerDb");
});

builder.Services.AddScoped<ITaskManagerRepository, TaskManagerRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowAnyMethod());
app.UseAuthorization();

app.MapControllers();

app.Run();
