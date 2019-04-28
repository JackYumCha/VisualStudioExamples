using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Jack.DataScience.Common;
using Jack.DataScience.Http.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using VsExample.AspAPI.Utils;
using Microsoft.Extensions.DependencyInjection.Extensions;
using VsExample.Data.MySQL;
using VsExample.Data.PostgresSQL;
using VsExample.Data.SQLServer;
using Jack.DataScience.Data.MongoDB;
using Swashbuckle.AspNetCore.Swagger;
using Jack.DataScience.Logging.Serilog;
using Serilog;

namespace VsExample.AspAPI
{
    public class Startup
    {
        static ILogger logger = Program.GetLogger();

        public Startup(IConfiguration configuration)
        {
            logger.Information($"{nameof(Startup)}");
            Configuration = configuration;
        }

        private static string CorsPolicy = "Cors";

        public IConfiguration Configuration { get; }
        public static IContainer ApplicationContainer;
        public const string SwaggerApiName = "VsExamples";

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            logger.Information($"{nameof(ConfigureServices)} Start");
            var environment = Environment.GetEnvironmentVariable(Program.ASPNETCORE_ENVIRONMENT);

            logger.Information($"AutoFacContainer Environment: {environment}");
            logger.Information($"AutoFacContainer Current Path: {AppContext.BaseDirectory}");
            AutoFacContainer autoFacContainer = new AutoFacContainer(environment);

            autoFacContainer.RegisterOptions<AuthOptions>();
            autoFacContainer.RegisterOptions<JwtSecretOptions>();

            autoFacContainer.ContainerBuilder.RegisterModule<JwtModule>();

            autoFacContainer.RegisterOptions<MySQLOptions>();
            autoFacContainer.ContainerBuilder.RegisterModule<MySQLDataModule>();
            autoFacContainer.RegisterOptions<PostgresSQLOptions>();
            autoFacContainer.ContainerBuilder.RegisterModule<PostgresSQLModule>();
            autoFacContainer.RegisterOptions<SQLServerOptions>();
            autoFacContainer.ContainerBuilder.RegisterModule<SQLServerModule>();


            autoFacContainer.RegisterOptions<MongoOptions>();
            autoFacContainer.ContainerBuilder.RegisterModule<MongoModule>();

            autoFacContainer.ContainerBuilder.RegisterInstance(Program.GetLogger());

            autoFacContainer.ContainerBuilder.Register(context =>
            {
                var httpContextAccessor = context.Resolve<IHttpContextAccessor>();
                return httpContextAccessor.HttpContext.Request.ReadJWTCookie();
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(options =>
               options.AddPolicy(CorsPolicy, builder =>
                   builder.SetIsOriginAllowed(url => true)
                       .AllowAnyHeader()
                       .AllowCredentials()
                       .AllowAnyMethod()
                   ));

            services.AddSwaggerGen(
                setup =>
                    setup.SwaggerDoc(SwaggerApiName,
                    new Info
                    {
                        Version = "1",
                        Title = "VsExampeles API",
                        Description = "VsExampeles API",
                        TermsOfService = "N/A"
                    })
                );


            services.AddMvc().AddJsonOptions(json =>
            {
                json.SerializerSettings.Error = OnJsonError;
                json.SerializerSettings.ContractResolver = new DefaultContractResolver();
                json.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            autoFacContainer.ContainerBuilder.Populate(services);
            ApplicationContainer = autoFacContainer.ContainerBuilder.Build();

            logger.Information($"{nameof(ConfigureServices)} End");
            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void OnJsonError(object source, ErrorEventArgs error)
        {
            logger.Information($"{nameof(OnJsonError)}");
            Debugger.Break();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            logger.Information($"{nameof(Configure)} Start");
            app.UseApiErrorMiddleware();
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            app.UseCors(CorsPolicy);

            // app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(setup => setup.SwaggerEndpoint($"/swagger/{SwaggerApiName}/swagger.json", "Job API"));
            app.UseMvc(routes =>
            {
                routes.MapSpaFallbackRoute("spaFallback", new { controller = "Home", action = "Spa" });
            });

            //app.UseMvc();

            logger.Information($"{nameof(Configure)} End");
        }
    }
}
