using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChyaKvAppSvc
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;

         Environment.SetEnvironmentVariable("AZURE_CLIENT_ID ", "971c306c-8ea5-4247-8a07-7732facc8d58");
         Environment.SetEnvironmentVariable("AZURE_CLIENT_SECRET", "UQF-z_kFJRr~lCAWrwHslg0f1Q75-4rvMw");
         Environment.SetEnvironmentVariable("AZURE_TENANT_ID", "4e6f57dc-a3d9-4a0c-818b-a7c1bb2b79f6");
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {

         services.AddControllers();
         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChyaKvAppSvc", Version = "v1" });
         });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         app.UseDeveloperExceptionPage();
         app.UseSwagger();
         app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChyaKvAppSvc v1"));

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
