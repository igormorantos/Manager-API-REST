using AutoMapper;
using EscNet.IoC.Hashers;
using Isopoh.Cryptography.Argon2;
using Manager.API.Configuration;
using Manager.API.Token;
using Manager.Core.Communication.Handlers;
using Manager.Core.Communication.Mediator.Interfaces;
using Manager.Core.Communication.Mediator;
using Manager.Core.Communication.MessagesNotifications;
using Manager.Infrastructure.Interfaces;
using Manager.Infrastructure.Repositories;
using Manager.Services.Interfaces;
using Manager.Services.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using Microsoft.Extensions.Hosting;
using Manager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Configure database context
builder.Services.AddDbContext<ManagerContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:USER_MANAGER"]);
});

// Configure Argon2 hasher
var argon2Config = new Argon2Config
{
    Type = Argon2Type.DataIndependentAddressing,
    Version = Argon2Version.Nineteen,
    Threads = Environment.ProcessorCount,
    TimeCost = int.Parse(builder.Configuration["Hash:TimeCost"]),
    MemoryCost = int.Parse(builder.Configuration["Hash:MemoryCost"]),
    Lanes = int.Parse(builder.Configuration["Hash:Lanes"]),
    HashLength = int.Parse(builder.Configuration["Hash:HashLength"]),
    //Salt = Encoding.UTF8.GetBytes(builder.Configuration["Hash:Salt"])
};
builder.Services.AddArgon2IdHasher(argon2Config);

// Configure AutoMapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register repositories and services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Configure authentication and authorization
builder.Services.AddControllers();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Manager API",
        Version = "v1",
        Description = "uma API REST de usuários, criada com boas práticas de desenvolvimento e arquitetura!",
        Contact = new OpenApiContact
        {
            Name = "Igor Moreira",
            Email = "igormsantos1@icloud.com",
            Url = new Uri("https://github.com/igormorantos/Manager-API-REST")
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor utilize Bearer <TOKEN>",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Configure MediatR
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Manager.API v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

// Define API endpoints using routing
app.MapControllers();

app.Run();