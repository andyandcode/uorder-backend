using Application.ActiveLogs;
using Application.AutoMapper;
using Application.Dishes;
using Application.Medias;
using Application.Menus;
using Application.Orders;
using Application.SystemSettings;
using Application.Tables;
using Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using Utilities.Constants;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UOrderDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString(SystemConstants.MainConnectionString)));

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
            });
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000") // Thêm origin của ứng dụng ReactJS
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    });
            });

            services.AddTransient<IDishService, DishService>();
            services.AddTransient<IMediaService, MediaService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ISystemSettingService, SystemSettingService>();
            services.AddTransient<ITableService, TableService>();
            services.AddTransient<IActiveLogService, ActiveLogService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())

            app.UseRouting();
            app.UseCors("AllowSpecificOrigins");
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