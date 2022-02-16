using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SmartSchool.WebAPI.Data;

namespace SmartSchool.WebApi
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
            services.AddCors(); 
            services.AddDbContext<SmartContext>(options => options.UseMySql(Configuration.GetConnectionString("MySqlConnection"))

            // services.AddDbContext<SmartContext>(
            //     context => context.UseMySql(Configuration.GetConnectionString("MySqlConnection"))
            );

             services.AddControllers()
                .AddNewtonsoftJson(opt 
                        => opt.SerializerSettings.ReferenceLoopHandling = 
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());                

            services.AddScoped<IRepository, Repository>();

            services.AddVersionedApiExplorer(options => 
            {
              options.GroupNameFormat = "'v'VVV";
              options.SubstituteApiVersionInUrl = true;     
            }).AddApiVersioning(options => {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            var apiProviderDescription = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(options => {
                foreach (var description in apiProviderDescription.ApiVersionDescriptions )
                {
                  options.SwaggerDoc(
                      description.GroupName,
                      new Microsoft.OpenApi.Models.OpenApiInfo(){
                   Title = "SmartSchool API",
                   Version = description.ApiVersion.ToString(),
                   TermsOfService = new Uri("http://TermosDeUso.com"),
                   Description = "Descriçao da WebAPI SmartSchool",
                   License =  new Microsoft.OpenApi.Models.OpenApiLicense
                   {
                       Name = "SmartSchool License",
                       Url = new Uri("http://mit.com")
                   },
                   Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = "Maria Julia Oliveira",
                    Email = "",
                    Url = new Uri("https://github.com/majulia/")
                }

                });  
            }
                
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);

                options.IncludeXmlComments(xmlCommentsFullPath);
            });
            services.AddCors();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiProviderDescription)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }

            // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin(). AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger()
            .UseSwaggerUI(options => {
                 foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                 { 
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                 }   

                options.RoutePrefix = "";
            });
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
