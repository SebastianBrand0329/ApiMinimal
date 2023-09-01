using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PropidadesApiMinimal.Data;
using PropidadesApiMinimal.Mapper;
using PropidadesApiMinimal.Models;
using PropidadesApiMinimal.Models.DTo;
using FluentValidation;
using System.Net;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar conexion

builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

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
app.MapGet("/api/propiedades", async (ApplicationDbContext context, ILogger<Program> logger) =>
{
    RespuestasApi respuestasApi = new ();
    // Usar el logger que ya está como inyección las dependencias
    logger.Log(LogLevel.Information, "Carga todas las propiedades");

    respuestasApi.Resultado = await context.propiedads.ToListAsync();
    respuestasApi.Success = true;
    respuestasApi.statusCode =  HttpStatusCode.OK;

    return Results.Ok(respuestasApi);
}).WithName("ObtenerPropiedades").Produces<RespuestasApi>(200);

//Obtener propiedad individual - GET - MapGet
app.MapGet("/api/propiedades/{id:int}", async (ApplicationDbContext context, int id) =>
{
    RespuestasApi respuestasApi = new ();

    respuestasApi.Resultado = await context.propiedads.FirstOrDefaultAsync(p => p.IdPropiedad == id);
    respuestasApi.Success = true;
    respuestasApi.statusCode= HttpStatusCode.OK;    

    return Results.Ok(respuestasApi);
}).WithName("ObtenerPropiedad").Produces<RespuestasApi>(200);


//Crear propiedad 

app.MapPost("/api/propiedades", async (ApplicationDbContext context, IMapper mapper, IValidator<CrearPropiedadDto> validator, [FromBody] CrearPropiedadDto crearPropiedad) =>
{
    RespuestasApi respuestasApi = new()
    {
        Success = false,
        statusCode = HttpStatusCode.BadRequest

    };
    var result = await validator.ValidateAsync(crearPropiedad);

    // Validar id de propiedad y que el nombre no esté vacio
    if (!result.IsValid)
    {
        respuestasApi.Errores.Add(result.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(respuestasApi);
    }

    if (await context.propiedads.FirstOrDefaultAsync(p => p.Nombre.ToLower() == crearPropiedad.Nombre.ToLower()) != null)
    {
        respuestasApi.Errores.Add(result.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(respuestasApi);
    }

    //Propiedad propiedad = new()
    //{
    //    Nombre = crearPropiedad.Nombre,
    //    Descripcion = crearPropiedad.Descripcion,
    //    Ubicacion = crearPropiedad.Ubicacion,
    //    Activa = crearPropiedad.Activa,
    //};

    Propiedad propiedad = mapper.Map<Propiedad>(crearPropiedad);

    //propiedad.IdPropiedad = DatosPropiedad.listaPropiedades.OrderByDescending(p => p.IdPropiedad).FirstOrDefault().IdPropiedad + 1;
    await context.propiedads.AddAsync(propiedad);
    await context.SaveChangesAsync();

    //DatosPropiedad.listaPropiedades.Add(propiedad);
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

    //return Results.CreatedAtRoute("ObtenerPropiedad", new { id = propiedadDto.IdPropiedad }, propiedadDto);

    respuestasApi.Resultado = propiedadDto;
    respuestasApi.Success = true;
    respuestasApi.statusCode = HttpStatusCode.Created;

    return Results.Ok(respuestasApi);

}).WithName("CrearPropiedad").Produces<RespuestasApi>(201).Produces(400);


//Actualizar

app.MapPut("/api/propiedades", async (ApplicationDbContext context, IMapper mapper, IValidator<ActualizarPropiedadDto> validator, [FromBody] ActualizarPropiedadDto actualizarPropiedadDto) =>
{
    RespuestasApi respuestasApi = new()
    {
        Success = false,
        statusCode = HttpStatusCode.BadRequest

    };
    var result = await validator.ValidateAsync(actualizarPropiedadDto);

    // Validar id de propiedad y que el nombre no esté vacio
    if (!result.IsValid)
    {
        respuestasApi.Errores.Add(result.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(respuestasApi);
    }

    if (await context.propiedads.FirstOrDefaultAsync(p => p.Nombre.ToLower() == actualizarPropiedadDto.Nombre.ToLower()) != null)
    {
        respuestasApi.Errores.Add(result.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(respuestasApi);
    }

    Propiedad propiedadBD = await context.propiedads.FirstOrDefaultAsync(p => p.IdPropiedad == actualizarPropiedadDto.IdPropiedad);
    propiedadBD.Nombre = actualizarPropiedadDto.Nombre;
    propiedadBD.Descripcion = actualizarPropiedadDto.Descripcion;
    propiedadBD.Ubicacion = actualizarPropiedadDto.Ubicacion;
    propiedadBD.Activa = actualizarPropiedadDto.Activa;

    await context.SaveChangesAsync();


    respuestasApi.Resultado = mapper.Map<PropiedadDto>(propiedadBD); ;
    respuestasApi.Success = true;
    respuestasApi.statusCode = HttpStatusCode.Created;

    return Results.Ok(respuestasApi);

}).WithName("ActualizarPropiedad").Produces<RespuestasApi>(200).Produces(400);

//Eliminar Propiedad

app.MapDelete("/api/propiedades/{id:int}", async (ApplicationDbContext context, int id) =>
{
    RespuestasApi respuestasApi = new()
    {
        Success = false,
        statusCode = HttpStatusCode.BadRequest
    };

    Propiedad propiedad = await context.propiedads.FirstOrDefaultAsync(p => p.IdPropiedad == id);

    if (propiedad != null)
    {
        context.propiedads.Remove(propiedad);
        await context.SaveChangesAsync();
        respuestasApi.Success = true;
        respuestasApi.statusCode = HttpStatusCode.NoContent;
        return Results.Ok(respuestasApi);
    }
    else
    {
        respuestasApi.Errores.Add("El ID de la propiedad no existe");
        return Results.BadRequest(respuestasApi);
    }



});

app.UseHttpsRedirection();
app.Run();

