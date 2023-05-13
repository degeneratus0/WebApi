using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Models;
using Microsoft.OpenApi.Models;
using WebApi.Interfaces;
using WebApi.Services;

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
            services.AddTransient<IFile<DataModel, DataDTO>, FCFileWork>();

            services.AddTransient<IData<Weighing>, WeighingsDBWork>();
            services.AddTransient<IData<Measure>, MeasuresDBWork>();

            services.AddTransient<IConverter<Weighing, WeighingDTO, WeighingDTOid>, WeighingsConverter>();
            services.AddTransient<IConverter<Measure, MeasureDTO, MeasureDTOid>, MeasuresConverter>();

            //string con = "server=127.0.0.1; port=3306; database=weighings; user=root; password=root";
            services.AddDbContext<WeighingsContext>(options => options.UseInMemoryDatabase("weighings"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WebApi",
                    Version = "v1",
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
