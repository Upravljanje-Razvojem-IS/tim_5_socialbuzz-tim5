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
using ForumService.Service;
using ForumService.Repository;
using ForumService.Repository.UserMock;
using ForumService.Logger;
using ForumService.Authorization;
using System.Reflection;
using System.IO;

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
                        Title = "Forum messaging API", 
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

                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name }.xml"; //refleksija
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments); //spaja vise stringova

                c.IncludeXmlComments(xmlCommentsPath); //da bi swagger mogao citati xml komenatare
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IAuthorization, Authorize>();
            services.AddScoped<IForumService, ForumsService>();
            services.AddScoped<IForumMessageService, ForumMessageService>();
            services.AddScoped<IForumRepository, ForumRepository>();
            services.AddScoped<IForumMessageRepository, ForumMessageRepository>();
            services.AddScoped<IUserMockRepository, UserMockRepository>();
            services.AddSingleton(typeof(ILoggerRepository<>), typeof(LoggerRepository<>));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/ForumApiSpecification/swagger.json", "Forum messaging API"));
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
