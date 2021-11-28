using IL41ML_HFT_2021221.Data;
using IL41ML_HFT_2021221.Logic;
using IL41ML_HFT_2021221.Repository;
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

namespace IL41ML_HFT_2021221.Endpoint
{
    public class Startup
    {
       /* public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }*/

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); // enable controllers
            //ioc
            services.AddTransient<IStockLogic, StockLogic>();
            services.AddTransient<IManagerLogic, ManagerLogic>();
            services.AddTransient<ICustomerLogic, CustomerLogic>();

            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IModelRepository, ModelRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<IShopRepository, ShopRepository>();


            services.AddSingleton<DbContext, CustomDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }/*
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }*/

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
