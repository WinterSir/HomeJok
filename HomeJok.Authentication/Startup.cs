using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HomeJok.Authentication
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

            //��ȡ���ݿ�����
            var connectionString = Configuration.GetSection("DB").Value;
            if (connectionString == "")
            {
                throw new Exception("���ݿ������쳣");
            }

            /**********************************IdentityServer4�־û�����**********************************/

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            //����û����������� ApplicationDbContext
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //����������������� ConfigurationDbContext���������������� PersistedGrantDbContext���û��־û�
            var builder = services.AddIdentityServer()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>
                {
                    builder.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                };
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                {
                    builder.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                };
                //token����
                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 30;
            })
            .AddAspNetIdentity<ApplicationUser>()
            .AddDeveloperSigningCredential();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            SeedData.InitData(app);
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

            app.UseIdentityServer(); //ʹ��IdentityServer4

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
