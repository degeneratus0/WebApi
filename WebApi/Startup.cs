using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Models;
using Microsoft.OpenApi.Models;
using WebApi.Services;
using WebApi.Models.DTOs;
using WebApi.Services.Interfaces;

namespace WebApi
{
    internal class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IFileRepository<DataModel, DataModelDTO>, FileRepository>();

            services.AddScoped<IRepository<Weighing>, WeighingsRepository>();
            services.AddScoped<IRepository<Measure>, MeasuresRepository>();

            services.AddTransient<IConverter<Weighing, WeighingDTO>, WeighingsConverter>();
            services.AddTransient<IConverter<Measure, MeasureDTO>, MeasuresConverter>();

            services.AddDbContext<WeighingsContext>(options => options.UseInMemoryDatabase("weighings"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v0.1", new OpenApiInfo
                {
                    Title = "WebApi",
                    Version = "v0.1",
                    Description = "A Web API with controllers implementing CRUD operations in different ways (i.e. in-memory list, static files, database)"
                });
                string xmlFile = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);
            });
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v0.1/swagger.json", "WebApi v0.1");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
