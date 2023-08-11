using BlazorCrud.Server.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//AGREGO LA DEPENDENCIA DE AUTOMAPPER
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//DE ESTA FORMA HAGO LA CONEXION AL SERVICIO DE LA BASE DE DATOS
builder.Services.AddDbContext<DbcrudBlazorContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("conectionSql"));
});


//habilitamos los cors ya que el proyecto de servidor y cliente se ejecutan en url distintas, activando los cors dejamos que se compartan informacion sin restriccion

builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("politica", app =>
    {
        app.AllowAnyOrigin();
        app.AllowAnyHeader();
        app.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//habilito los cors

app.UseCors("politica");

app.UseAuthorization();

app.MapControllers();

app.Run();

