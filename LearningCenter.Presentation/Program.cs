using System.Reflection;
using _1_API.Mapper;
using _2_Domain.IAM.CommandServices;
using Application;
using Application.IAM.CommandServices;
using Application.IAM.QueryServices;
using Domain;
using Infraestructure;
using Infraestructure.Contexts;
using LearningCenter.Domain.IAM.Repositories;
using LearningCenter.Domain.IAM.Services;
using LearningCenter.Infraestructure.IAM.Persistence;
using LearningCenter.Presentation.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Ad cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy", 
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "APis form mange learning center",
        Description = "An ASP.NET Core Web API for managing ToDo tutorial",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});

//dependency inyection
builder.Services.AddScoped<ITutorialRepository, TutorialRepository>();
builder.Services.AddScoped<ITutorialCommandService, TutorialCommandService>();
builder.Services.AddScoped<ITutorialQueryService, TutorialQueryService>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserCommandService,UserCommandService>();
builder.Services.AddScoped<IEncryptService,EncryptService>();
builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddScoped<IUserQueryService,UserQueryService>();

//AUtomapper
builder.Services.AddAutoMapper(
    typeof(RequestToModels),
    typeof(ModelsToRequest),
    typeof(ModelsToResponse));

//Conexion a MySQL 
var connectionString = builder.Configuration.GetConnectionString("learningCenterConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

builder.Services.AddDbContext<LearningCenterContext>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    });


//Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<LearningCenterContext>())
{
    context.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<AuthenticationMiddleware>();

app.UseCors("AllowAllPolicy");

app.Run();