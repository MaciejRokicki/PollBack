using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using FluentValidation.AspNetCore;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PollBack.Core.AppSettings;
using PollBack.Core.Validators;
using PollBack.Infrastructure;
using PollBack.Infrastructure.Data;

namespace PollBack.Web.IoC
{
    public static class ServicesExtension
    {
        public static void RegisterFluentValidation(this WebApplicationBuilder builder)
        {
            builder.Services.AddFluentValidation(x =>
            {
                x.RegisterValidatorsFromAssemblyContaining<SignInRequestValidator>();
                x.DisableDataAnnotationsValidation = true;
                x.ImplicitlyValidateChildProperties = true;
            });
        }

        public static void RegisterSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "title", Version = "v1" });
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

        public static void RegisterCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CORS",
                    x =>
                    {
                        x.WithOrigins(builder.Configuration.GetValue<string>("Frontend:url"))
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
            });
        }

        public static void RegisterDbContexts(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbConntectionString"));
            });
        }

        public static void RegisterConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<SecuritySettings>(builder.Configuration.GetSection(SecuritySettings.Name));
        }

        public static void RegisterAutofac(this WebApplicationBuilder builder)
        {
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterMediatR(typeof(Program).Assembly);
                containerBuilder.RegisterAutoMapper(typeof(Program).Assembly);

                containerBuilder.RegisterModule(new InfrastructureModule());
                containerBuilder.RegisterModule(new CoreModule());
            });
        }
    }
}
