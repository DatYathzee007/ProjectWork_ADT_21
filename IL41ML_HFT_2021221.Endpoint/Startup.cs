using IL41ML_HFT_2021221.Data;
using IL41ML_HFT_2021221.Endpoint.Services;
using IL41ML_HFT_2021221.Logic;
using IL41ML_HFT_2021221.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            services.AddTransient<IExistingData, ExistingData>();
            services.AddTransient<IStockLogic, StockLogic>();
            services.AddTransient<IManagerLogic, ManagerLogic>();
            services.AddTransient<ICustomerLogic, CustomerLogic>();

            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IModelRepository, ModelRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<IShopRepository, ShopRepository>();
            
            services.AddTransient<DbContext, CustomDbContext>();
            //services.AddSingleton<DbContext, CustomDbContext>();
            services.AddSignalR();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GLobalPhone.Endpoint", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GLobalPhone.Endpoint v1"));
            }
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));
            /*
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }*/

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseCors(x => x
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:58978"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
