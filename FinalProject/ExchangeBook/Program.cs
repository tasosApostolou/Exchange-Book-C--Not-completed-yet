
using AutoMapper;
using ExchangeBook.Configuration;
using ExchangeBook.Data;
using ExchangeBook.Helpers;
using ExchangeBook.Repositories;
using ExchangeBook.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;

namespace ExchangeBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            });

            // Add services to the container.
            var connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<BookExchangeDbContext>(options => options.UseSqlServer(connString));
            builder.Services.AddScoped<IApplicationService, ApplicationService>();
            builder.Services.AddRepositories();
            //builder.Services.AddAutoMapper(typeof(MapperConfig)); // Singleton
            //builder.Services.AddControllers()

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped(provider =>
               new MapperConfiguration(cfg =>
               {
                   cfg.AddProfile(new MapperConfig());
               })
           .CreateMapper());
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = false,
                    //SignatureValidator = (token, parameters) => new JsonWebToken(token)
                    SignatureValidator = (token, validator) => { return new JsonWebToken(token); }
                };
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
               options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
           });

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "School API", Version = "v1" });
                // Non-nullable reference are properly documented
                options.SupportNonNullableReferenceTypes();
                options.OperationFilter<AuthorizeOperationFilter>();
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = "JWT"
                    });
            });
            builder.Services.AddCors(options => {
                options.AddPolicy("AllowAll",
                    b => b.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin());
            });

            builder.Services.AddCors(options => {
                options.AddPolicy("AngularClient",
                    b => b.WithOrigins("http://localhost:4200") // Assuming Angular runs on localhost:4200
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlerMiddleware>();


            app.MapControllers();

            app.Run();
        }
    }
}
