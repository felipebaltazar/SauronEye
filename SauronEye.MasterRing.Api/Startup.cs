using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SauronEye.AllDomain.Domain.Dtos;
using SauronEye.MasterRing.Api.Hosted;
using System.Collections.Concurrent;

namespace SauronEye
{
    public class Startup
    {
        public static ConcurrentDictionary<string, LoggedUser> LoggedUsers;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            LoggedUsers = new ConcurrentDictionary<string, LoggedUser>();
        }

        public IConfiguration Configuration { get; }

        public static void AddUser(LoggedUser user)
        {
            if (LoggedUsers.ContainsKey(user.Username))
            {
                LoggedUser userRemoved;
                LoggedUsers.TryRemove(user.Username, out userRemoved);
            }
            LoggedUsers.TryAdd(user.Username, user);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<GetProblemsService>();
            services.AddHostedService<PostPoblemService>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
