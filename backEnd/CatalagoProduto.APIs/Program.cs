using CatalogoProduto.APIs.Models.Mapping;
using CatalogoProduto.HelpConfig.HelpStartup;
using CatalogoProduto.Infra.Data;
using CatalogoProduto.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DbConnectionFactory>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new DbConnectionFactory(connectionString);
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CatalogoProduto.APIs",
        Version = "v1",
    });

    c.MapType<bool>(() => new OpenApiSchema
    {
        Type = "boolean",
        Default = new OpenApiBoolean(false)
    });

    var xmlFile = "CatalogoProduto.APIs.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddAuthorization();
builder.Services.AddControllers();

HelpStartup.ConfigureScoped(builder.Services);

builder.Services.AddScoped<ProdutoRepository>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        builder => builder.WithOrigins("http://localhost:4200") 
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); 
app.UseAuthorization(); 

app.UseHttpsRedirection();
app.UseCors("AllowAngular");

app.MapControllers();

app.Run();