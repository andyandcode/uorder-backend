using Application.Accounts;
using Application.AutoMapper;
using Application.DiscountCodes;
using Application.Dishes;
using Application.Files;
using Application.Jwt;
using Application.Menus;
using Application.Orders;
using Application.Payment;
using Application.SystemSettings;
using Application.Tables;
using Azure.Storage.Blobs;
using Data.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Utilities.Constants;
using WebApi.Extension;

namespace WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UOrderDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString(SystemConstants.MainConnectionString)));

            services.AddControllers().AddNewtonsoftJson().AddJsonOptions(
                options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "UOrder API",
                    Version = "v1",
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                c.IncludeXmlComments(xmlFilename);
                c.EnableAnnotations();
                // Bỏ qua quyền lợi cho Swagger UI
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000", "http://192.168.0.101:80", "http://192.168.0.101:90", "http://192.168.0.101")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]))
                };
            });
            services.AddAuthorization();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IDishService, DishService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ISystemSettingService, SystemSettingService>();
            services.AddTransient<ITableService, TableService>();
            services.AddTransient<IAccountService, Application.Accounts.AccountService>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IFileService, Application.Files.FileService>();
            services.AddTransient<IDiscountCodeService, DiscountCodeService>();
            services.AddSingleton(x => new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=uorderfile123;AccountKey=6zPn/Y6oku+KsZH+EZ6gFGDgZYJ0v1wPzrwaYQWAinAJfbRoQrmsVyZOhElOpZkMaqXTMAp+F1/r+AStEN5O4w==;EndpointSuffix=core.windows.net"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseErrorHandlerMiddleware();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<OrderHub>("/bookingHub");
                endpoints.MapControllers();
            });
            //}
            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
}