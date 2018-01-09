using ItemsBasket.AuthenticationService.Services;
using ItemsBasket.AuthenticationService.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace ItemsBasket.AuthenticationService
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                    new Info
                    {
                        Version = "v1",
                        Title = "Authentication Service API",
                        Description = "Allows CRUD operations for users.",
                        TermsOfService = "None",
                        Contact = new Contact { Name = "Andreas Antoniou", Email = "andreas_antoniou1@hotmail.com" }
                    });

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "ItemsBasket.AuthenticationService.xml");
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSingleton<IUsersValidator, UsersValidator>();
            services.AddSingleton<IUsersRepository, UsersRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication Service API V1");
            });

            app.UseMvc();
        }
    }
}
