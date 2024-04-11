using AutoMapper;
using EscNet.DependencyInjection.IoC.Cryptography;
using Manager.API.Token;
using Manager.API.ViewModels;
using Manager.API.ViewModes;
using Manager.Domain.Entity;
using Manager.Infrastructure.Context;
using Manager.Infrastructure.Interfaces;
using Manager.Infrastructure.Repositories;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using Manager.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace Manager.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton(cfg => Configuration);

            #region Jwt

            var secretKey = Configuration["Jwt:Key"];

            services.AddAuthentication(x =>
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
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });

            #endregion

            #region AutoMapper

            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>().ReverseMap();
                cfg.CreateMap<CreateUserViewModel, UserDTO>().ReverseMap();
                cfg.CreateMap<UpdateUserViewModel, UserDTO>().ReverseMap();
            });

            services.AddSingleton(autoMapperConfig.CreateMapper());

            #endregion

            #region Database

            services.AddDbContext<ManagerContext>(options => options
                .UseSqlServer(Configuration["ConnectionStrings:USER_MANAGER"])
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())),
            ServiceLifetime.Transient);

            #endregion

            #region Repositories

            services.AddScoped<IUserRepository, UserRepository>();

            #endregion

            #region Services

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            #endregion

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Manager API",
                    Version = "v1",
                    Description = "API constru�da na serie de v�deos no canal Lucas Eschechola.",
                    Contact = new OpenApiContact
                    {
                        Name = "Lucas Eschechola",
                        Email = "lucas.gabriel@eu.com",
                        Url = new Uri("https://eschechola.com.br")
                    },
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

            #endregion

            #region Hash

            /* var config = new Argon2Config
             {
                 Type = Argon2Type.DataIndependentAddressing,
                 Version = Argon2Version.Nineteen,
                 TimeCost = int.Parse(Configuration["Hash:TimeCost"]),
                 MemoryCost = int.Parse(Configuration["Hash:MemoryCost"]),
                 Lanes = int.Parse(Configuration["Hash:Lanes"]),
                 Threads = Environment.ProcessorCount,
                 Salt = Encoding.UTF8.GetBytes(Configuration["Hash:Salt"]),
                 HashLength = int.Parse(Configuration["Hash:HashLength"])
             };

             services.AddArgon2IdHasher(config);*/

            #endregion

            #region Mediator

            /* services.AddMediatR(typeof(Startup));
             services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
             services.AddScoped<IMediatorHandler, MediatorHandler>();
            */
            #endregion

            #region Cryptography

            services.AddRijndaelCryptography(Configuration["Cryptography"]);

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Manager.API v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            //app.UseCustomExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
