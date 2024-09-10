using Back.Model;
using Microsoft.EntityFrameworkCore;
using Notes.BusinessLayer.Interfaces;
using Notes.BusinessLayer.Services;
using Notes.DataAccsessLayer.Interfaces;
using Notes.DataAccsessLayer.Repositories;
using Presentation.Mapping;


var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddDbContext<DbContextNotes>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnect")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyCors",
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddScoped<IUnitOfWork, UnitOfWorkEF>();
builder.Services.AddScoped<INoteService, NoteService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCors");

app.MapControllers();

app.Run();