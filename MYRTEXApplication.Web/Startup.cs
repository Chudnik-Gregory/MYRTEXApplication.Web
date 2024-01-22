using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using MYRTEX.Infrastructure;
using MYRTEX.Application.Repositories;
using MYRTEX.Infrastructure.Repositories;
using MediatR;
using MYRTEX.Application.Queries;
using System.Collections.Generic;
using MYRTEX.Infrastructure.Queries;
using MYRTEX.Application.Commands;
using MYRTEX.Infrastructure.Commands;
using MYRTEX.Domain.Models;
using Microsoft.Extensions.Logging;

namespace MYRTEXApplication.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvcCore()
                .AddApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MYRTEXApplication API", Version = "v1" });
            });

            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("SqlConnectionString")));

            services.AddMediatR(typeof(Startup).Assembly);
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IRequestHandler<GetEmployeesQuery, List<GetEmployeeResult>>, GetEmployees>();
            services.AddScoped<IRequestHandler<AddEmployeeCommand, EmployeeEntity>, AddEmployeeHandler>();
            services.AddScoped<IRequestHandler<PutEmployeeCommand, EmployeeEntity>, PutEmployeeHandler>();
            services.AddScoped<IRequestHandler<DeleteEmployeeCommand, Unit>, DeleteEmployeeHandler>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSwaggerUI",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                dbContext.Database.EnsureCreated();
            }

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "MYRTEXApplication API v1");
                x.RoutePrefix = "swagger";
            });
            app.UseCors("AllowSwaggerUI");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
