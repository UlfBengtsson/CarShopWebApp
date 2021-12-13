using CarShopApp.Models;
using CarShopApp.Models.Data;
using CarShopApp.Models.Repos;
using CarShopApp.Models.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp
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
            services.AddDbContext<ShopDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddScoped<ICarsRepo, InMemoryCarsRepo>();
            services.AddScoped<ICarsRepo, DatabaseCarsRepo>();
            //services.AddScoped<IBrandRepo, DatabaseBrandRepo>();
            //services.AddScoped<IGenericRepo<Brand, int>, DatabaseBrandRepo>();
            services.AddScoped<IGenericRepo<Brand, int>, DatabaseGenericRepo<Brand, int>>();
            //services.AddScoped<IInsuranceRepo, DatabaseInsuranceRepo>();
            //services.AddScoped<IGenericRepo<Insurance, int>, DatabaseInsuranceRepo>();
            services.AddScoped<IGenericRepo<Insurance, int>, DatabaseGenericRepo<Insurance, int>>();
            
            
            services.AddScoped<ICarsService, CarsService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IInsuranceService, InsuranceService>();

            services.AddMvc().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
