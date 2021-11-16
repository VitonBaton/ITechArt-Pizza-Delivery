using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Domain.Services;
using ITechArtPizzaDelivery.Infrastructure.Contexts;
using ITechArtPizzaDelivery.Infrastructure.Repositories;
using ITechArtPizzaDelivery.Infrastructure.Repositories.EFRepositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ITechArtPizzaDelivery.Web
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
            services.AddAutoMapper(typeof(Startup));
            // Domain
            services.AddScoped<IPizzasService, PizzasService>();
            services.AddScoped<IIngredientsService, IngredientsService>();
            services.AddScoped<IPromocodesService, PromocodesService>();
            // Infrastructure
            services.AddScoped<IPizzasRepository, PizzasRepository>();
            services.AddScoped<IIngredientsRepository, IngredientsRepository>();
            services.AddScoped<IPromocodesRepository, PromocodesRepository>();

            services.AddDbContext<PizzaDeliveryContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ITechArtPizzaDelivery.Web", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ITechArtPizzaDelivery.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
