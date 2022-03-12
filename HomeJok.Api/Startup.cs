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
            //ע��appsettings.json������
            services.AddSingleton(new Appsettings(Configuration));

            //ע��EFCore PostgreSQL
            services.AddDbContext<WinterSirContext>(options =>
            {
                options.UseNpgsql(Appsettings.app("DB"), efoptions =>
                {
                    //ָ��Ǩ���ļ������Ŀ
                    efoptions.MigrationsAssembly("HomeJok.Repository");
                });
            });

            services.AddControllers(options =>
            {
                options.Filters.Add<ExceptionFilter>(); //�쳣������
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HomeJok.Api", Version = "v1" });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //ע�뷺�Ͳִ�
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>));

            //ע��AOP������
            builder.RegisterType(typeof(ServiceAop));

            //���򼯷�������ע��
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
