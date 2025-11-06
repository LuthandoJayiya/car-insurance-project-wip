using LLJ_CarInsuranceMS_RESTAPI.AuthModels;
using log4net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace LLJ_CarInsuranceMS_RESTAPI;

/// <summary>
/// Program.cs
/// </summary>
public class Program
{
    private static readonly ILog logger = LogManager.GetLogger("Program.main method");

    /// <summary>
    /// Main method.
    /// </summary>
    public static void Main(string[] args)
    {
        logger.Info("REST API Started!");
        var builder = WebApplication.CreateBuilder(args);

        //Inject Application Settings
        builder.Services.AddDbContext<Models.LLJ_CarInsuranceMS_EFDBContext>(
            options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("CRUDConnection"));
            });

        builder.Services.AddControllers();
        builder.Services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

        builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));

        builder.Services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AuthenticationContext>();

        string tmpKeyIssuer = builder.Configuration.GetSection("ApplicationSettings:JWT_Site_URL").Value;

        string tmpKeySign = builder.Configuration.GetSection("ApplicationSettings:SigningKey").Value;

        var key = Encoding.UTF8.GetBytes(tmpKeySign);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>{
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = tmpKeyIssuer,
                    ValidAudience = tmpKeyIssuer,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false; //For dev only
            options.Password.RequireLowercase = false; //For dev only
            options.Password.RequireUppercase = false; //For dev only
            options.Password.RequireNonAlphanumeric = false; //For dev only
            options.Password.RequiredLength = 6; //12 for production
        });

        //builder.Services.AddCors(c =>
        //{
        //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
        //});
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin",
                builder =>
                {
                    builder.WithOrigins("http://127.0.0.1:5500")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();

                    builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
        });

        // Add services to the container.


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                new string[] {}
        }});});

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("AllowOrigin");
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

       

        app.Run();
    }
}
