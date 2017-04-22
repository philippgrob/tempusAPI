using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace tempusAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddCors();

        // Add framework services.
        services
                .AddMvc()
                .AddMvcOptions(action =>
                     {
                         action.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
                         action.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                     }).AddJsonOptions(o => 
                     {
                         o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                     });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "http://localhost:5000/",
                RequireHttpsMetadata = false,
                ApiName = "api1"
            });


            app.UseCors(builder =>
                          builder.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod());


            app.UseSwagger();
            app.UseSwaggerUi();

            app.UseMvc();
        }
    }
}
