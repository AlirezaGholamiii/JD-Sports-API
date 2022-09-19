using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using JdRunner.Database;
using JdRunner.Filters;
using JdRunner.Services;
using JdRunner.Services.Packages;
using JdRunner.Services.Packages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdRunner
{
    public class Startup
    {
        readonly string AllowOrigin = "my allow origin";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Settings._config = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowOrigin,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                                  });

            });
            services.AddControllers();
            services.AddDbContext<ModelContext>(options => options.UseOracle(Configuration.GetConnectionString("POSConStr")));
            services.AddTransient<IProductExclusionPKService, ProductExclusionPKService>();
            services.AddTransient<IAuthenticationDataService, AuthenticationDataService>();
            services.AddScoped<ValidateSessionAsyncActionFilter>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductExclusion", Version = "v1" });
                c.OperationFilter<DefaultHeaderFilter>();

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            if (Configuration.GetSection("appSettings").GetValue<bool>("EnableSwaggerDoc"))
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductExclusion v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(AllowOrigin);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
