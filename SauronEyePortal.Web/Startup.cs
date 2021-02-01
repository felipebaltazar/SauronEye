using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SauronEyePortal.Web
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
            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });

            StartNodeRed().GetAwaiter().GetResult();
        }

        private async Task StartNodeRed()
        {
            try
            {
                var str_Path = "check_nodejs.bat";
                var processInfo = new ProcessStartInfo(str_Path);
                processInfo.UseShellExecute = false;
                using (var batchProcess = new Process())
                {
                    batchProcess.StartInfo = processInfo;
                    batchProcess.Start();
                    batchProcess.WaitForExit();
                    while (!batchProcess.HasExited)
                    {
                        await Task.Delay(200).ConfigureAwait(false);
                    }
                }

                str_Path = "start_rednode_server.bat";
                processInfo = new ProcessStartInfo(str_Path);
                processInfo.UseShellExecute = false;
                using (var batchProcess = new Process())
                {
                    batchProcess.StartInfo = processInfo;
                    batchProcess.Start();
                }
            }
            catch (Exception ex)
            {

            }
           
        }
    }
}
