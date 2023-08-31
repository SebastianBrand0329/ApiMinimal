using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PropidadesApiMinimal.Data;
using PropidadesApiMinimal.Mapper;
using PropidadesApiMinimal.Models;
using PropidadesApiMinimal.Models.DTo;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Añadir el automapper
builder.Services.AddAutoMapper(typeof(ConfiguracionMapper));

//Añadir validación
builder.Services.AddValidatorsFromAssemblyContaining<Program>();    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Obtener todas las propiedades - GET - MapGet
app.MapGet("/api/propiedades", (ILogger<Program> logger) =>
{
    // Usar el logger que ya está como inyección las dependencias
    logger.Log(LogLevel.Information, "Carga todas las propiedades");

    return Results.Ok(DatosPropiedad.listaPropiedades);
}).WithName("ObtenerPropiedades").Produces<Propiedad>(201);

//Obtener propiedad individual - GET - MapGet
app.MapGet("/api/propiedades/{id:int}", (int id) =>
{
    return Results.Ok(DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == id));
}).WithName("ObtenerPropiedad").Produces<Propiedad>(201);


//Crear propiedad 

app.MapPost("/api/propiedades", (IMapper mapper, IValidator<CrearPropiedadDto> validator, [FromBody] CrearPropiedadDto crearPropiedad) =>
{
    var result = validator.ValidateAsync(crearPropiedad).GetAwaiter().GetResult();

    // Validar id de propiedad y que el nombre no esté vacio
    if (!result.IsValid)
    {
        return Results.BadRequest(result.Errors. FirstOrDefault().ToString);
    }

    if (DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.Nombre.ToLower() == crearPropiedad.Nombre.ToLower()) != null)
    {
        return Results.BadRequest("El nombre de la propiedad ya existe");
    }

    //Propiedad propiedad = new()
    //{
    //    Nombre = crearPropiedad.Nombre,
    //    Descripcion = crearPropiedad.Descripcion,
    //    Ubicacion = crearPropiedad.Ubicacion,
    //    Activa = crearPropiedad.Activa,
    //};

    Propiedad propiedad = mapper.Map<Propiedad>(crearPropiedad);

    propiedad.IdPropiedad = DatosPropiedad.listaPropiedades.OrderByDescending(p => p.IdPropiedad).FirstOrDefault().IdPropiedad + 1;
    DatosPropiedad.listaPropiedades.Add(propiedad);
    //return Results.Ok(DatosPropiedad.listaPropiedades);   
    //return Results.Created($"/api/propiedades/{propiedad.IdPropiedad}", propiedad);
    //PropiedadDto propiedadDto = new()
    //{
    //    IdPropiedad = propiedad.IdPropiedad,
    //    Nombre = propiedad.Nombre,
    //    Descripcion = propiedad.Descripcion,
    //    Ubicacion = propiedad.Ubicacion,
    //    Activa = propiedad.Activa,
    //};
    PropiedadDto propiedadDto = mapper.Map<PropiedadDto>(propiedad);

    return Results.CreatedAtRoute("ObtenerPropiedad", new { id = propiedadDto.IdPropiedad }, propiedadDto);
}).WithName("CrearPropiedad").Produces<Propiedad>(201).Produces(400);


app.UseHttpsRedirection();
app.Run();-

