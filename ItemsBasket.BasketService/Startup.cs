﻿using ItemsBasket.BasketService.Services;
using ItemsBasket.BasketService.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace ItemsBasket.BasketService
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Version = "v1",
                        Title = "Basket Service API",
                        Description = "Allows CRUD operations for user baskets.",
                        TermsOfService = "None",
                        Contact = new Contact { Name = "Andreas Antoniou", Email = "andreas_antoniou1@hotmail.com" }
                    });

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "ItemsBasket.BasketService.xml");
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSingleton<IBasketItemsRepository, BasketItemsRepository>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket Service API V1");
            });

            app.UseMvc();
        }
    }
}