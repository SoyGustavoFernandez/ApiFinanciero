using AutoMapper;
using DataBackend.Models;
using DependencyInjectionBackEnd;
using Microsoft.OpenApi.Models;
using RepositoryBackEnd;
using ServicesBackEnd;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile()); 
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

DependencyInjectionConfiguration.ConfigureDependencyInjection(builder.Services);

//builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
//builder.Services.AddScoped<IPersonaService, PersonaService>();

builder.Services.AddDbContext<RetoBackendContext>();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reto Backend", Version = "v1", Contact = new OpenApiContact { Name = "Gustavo Fernández Saavedra", Email = "soyGustavoFernandez@gmail.com", Url = new Uri("https://www.linkedin.com/in/gustavofernandezsaavedra") } });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"); });

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();