using Autofac;
using Autofac.Core;
using Autofac.Extras.DynamicProxy;
using HomeJok.Api;
using HomeJok.Repository;
using HomeJok.Repository.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace HomeJok.Api
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
            //注入appsettings.json帮助类
            services.AddSingleton(new Appsettings(Configuration));

            //注入EFCore PostgreSQL
            services.AddDbContext<WinterSirContext>(options =>
            {
                options.UseNpgsql(Appsettings.app("DB"), efoptions =>
                {
                    //指定迁移文件存放项目
                    efoptions.MigrationsAssembly("HomeJok.Repository");
                });
            });

            services.AddControllers(options =>
            {
                options.Filters.Add<ExceptionFilter>(); //异常过滤器
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HomeJok.Api", Version = "v1" });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //注入泛型仓储
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>));

            //注册AOP拦截器
            builder.RegisterType(typeof(ServiceAop));

            //程序集反射批量注入
            var assemblysService = Assembly.LoadFrom(Path.Combine(AppContext.BaseDirectory, "HomeJok.Services.dll"));
            builder.RegisterAssemblyTypes(assemblysService)
                   .AsImplementedInterfaces().EnableInterfaceInterceptors().InterceptedBy(typeof(ServiceAop))
                   .InstancePerDependency();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            //app.UseMiddleware<TestExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeJok.Api v1"));
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
