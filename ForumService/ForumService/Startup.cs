using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumService.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ForumService
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

            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters(); //podrska za application/xml

            services.AddDbContext<DataContext>(opt => {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("ForumApiSpecification",
                    new OpenApiInfo 
                    { 
                        Title = "Forum messaging", 
                        Version = "v1" ,
                        Description = "API koji omogucava javnu komunikaciju izmedju korisnika u posebnim, forum grupama.",
                        Contact = new OpenApiContact{ 
                            Name = "Sara Lazarevic",
                            Email = "sara.lola.2@gmail.com"
                        },
                        License = new OpenApiLicense { 
                            Name = "FTN licenca"
                        }
                    });
            });
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(setupAction => {
                    setupAction.SwaggerEndpoint("/swagger/RatingsApiSpecification/swagger.json", "Forum messaging API");
                    setupAction.RoutePrefix = ""; //odmah mi otvori swagger dokumentaciju kada pokrenem servis u browseru
                });
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("There has been error. Please try later!");
                    });
                });
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
