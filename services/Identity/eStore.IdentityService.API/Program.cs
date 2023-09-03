using eStore.IdentityService.API.Services;
using eStore.IdentityService.Core.DataSource;
using eStore.Shared.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        var builder = WebApplication.CreateBuilder(args);

        IConfigurationRoot configurationRoot = builder.Configuration
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .SetBasePath(Directory.GetCurrentDirectory()).Build();

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultSqlServer");
        builder.Services.AddDbContext<SsoDbContext>(builder =>
        {
            builder.UseSqlServer(connectionString, builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        }, ServiceLifetime.Transient);

        builder.Services.AddSingleton<IRuntimeSettings, RuntimeSettings>();
        // Add functionality to inject IOptions<T>
        builder.Services.AddOptions();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
        });

        // Add our Config object so it can be injected
        builder.Services.Configure<RuntimeSettings>(configurationRoot.GetSection("RuntimeSettings"));
        builder.Services.AddTransient<ISsoDataSource, SsoDataSource>();
        builder.Services.AddScoped<IUserServices, UserServices>();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "eStore SSO API",
            });

            //options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
            //{
            //    Name = "Authorization",
            //    Type = SecuritySchemeType.Http,
            //    Scheme = "basic",
            //    In = ParameterLocation.Header,
            //    Description = "Basic Authorization header using the Bearer scheme."
            //});

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
                + " \r\n\r\n Enter 'X-Token-System' [space] and then your token in the text input below." 
                + "\r\n\r\nExample: \"Bearer 12345abcdef\"",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme{
                        Reference = new OpenApiReference{
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });

        //builder.Services.AddAuthentication("BasicAuthentication")
        //    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
        builder.Services.AddScoped<IBasicAuthenticationServices, BasicAuthenticationServices>();

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.Audience = "estoreapi";
            x.ClaimsIssuer = "api.store.com";
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configurationRoot["JwtToken:Key"])),
                TokenDecryptionKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configurationRoot["JwtToken:Key"])),
                ValidateIssuer = false,
                ValidateAudience = true,
                RequireSignedTokens = true,
            };
            x.Events = new JwtBearerEvents
            {
                OnTokenValidated = ctx =>
                {
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = ctx =>
                {
                    return Task.CompletedTask;
                }
            };
        });


        builder.Services.AddTransient<ITokenDataSource, TokenDataSource>();
        builder.Services.AddTransient<ITokenAuthenticationServices, TokenAuthenticationServices>();
        builder.Services.AddAntiforgery(options =>
        {
            options.HeaderName = "X-XSRF-TOKEN";
            options.Cookie.Name = "__Host-X-XSRF-TOKEN";
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });
        var app = builder.Build();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
        // Configure the HTTP request pipeline.
        app.UseSwagger(options =>
        {
            options.SerializeAsV2 = true;
        });
        app.UseSwaggerUI();
        app.UseCors(MyAllowSpecificOrigins);
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapDefaultControllerRoute();
        app.MapControllers();
        app.Run();
    }
}