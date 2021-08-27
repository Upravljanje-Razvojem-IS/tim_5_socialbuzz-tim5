using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PostMicroservice.Auth;
using PostMicroservice.Data;
using PostMicroservice.Data.ContentRepository;
using PostMicroservice.Data.Image;
using PostMicroservice.Data.PostRepository;
using PostMicroservice.Database;
using PostMicroservice.FakeLogger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PostMicroservice
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
            services.AddControllers(
                setup =>
                {
                    setup.ReturnHttpNotAcceptable = true;           
                }
                ).AddXmlDataContractSerializerFormatters();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PostsDB")));
            services.AddScoped<IPictureRepository, PictureRepository>();
            services.AddHttpContextAccessor();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IContentRepository, ContentRepository>();
            services.AddScoped<IAuthService, AuthService>();



            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("PostOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "PostMicroservice API",
                        Version = "1",
                        Description = "This API allows you to create new post, modify, view, and delete existing posts. It also allows you to add, edit, delete and display content and photos in a post.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Milica Despotović",
                            Email = "milica.despotovic981@gmail.com",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "FTN licence",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                    });
               var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name }.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                setupAction.IncludeXmlComments(xmlCommentsPath);
            });


            services.AddSingleton<IFakeLoggerRepository, FakeLoggerRepository>();
            services.AddSingleton<IUserAccountMockRepository, UserAccountMockRepository>();
            services.AddSingleton< IObjectForSaleMockRepository, ObjectForSaleMockRepository >();
            services.AddSingleton<IFollowMockRepository, FollowMockRepository>();
            services.AddSingleton<IBlockMockRepository, BlockMockRepository>();



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
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An error has occured.Try later");
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

            app.UseSwagger();
          

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/PostOpenApiSpecification/swagger.json", "PostMicroservice API");
                setupAction.RoutePrefix = "";
            });

        }
    }
}
