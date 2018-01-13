using ItemsBasket.BasketService.Services;
using ItemsBasket.BasketService.Services.Interfaces;
using ItemsBasket.AuthenticationService.Configuration;
using ItemsBasket.AuthenticationService.Services;
using ItemsBasket.AuthenticationService.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Text;

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
            services
                .AddSwaggerGen(c =>
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
                })
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
                        ValidIssuer = "localhost:57754",
                        ValidAudience = "localhost:49860",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigProvider.SecurityKey))
                    };
                });


            services.AddSingleton<IBasketItemsRepository, BasketItemsRepository>();
            services.AddSingleton<IAuthorizationLayer, AuthorizationLayer>();

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

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}