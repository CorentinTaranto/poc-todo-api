using System;
namespace Api
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.DataProtection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.InMemory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using EntityFramework;
    using Microsoft.OpenApi.Models;
    using Api.App_Start;
    using Core.Services;

    public partial class Startup
	{
        private readonly bool showSwagger;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.showSwagger = this.Configuration.GetValue<bool>("WebAPI:ShowSwagger");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            TodoContext context,
            IBoardService boardService,
            ITodoService todoService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ingenum.Case.Api v1"));
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            app.UseResponseCompression();


            SeedDatabase.Initialize(context, boardService, todoService).Wait();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHealthChecks();
            services.AddResponseCompression();

            services.AddAntiforgery();

            

            services.AddAutoMapper(typeof(MapperProfile));

            // Injection of the database context
            services.AddDbContext<TodoContext>(options =>
                options.UseInMemoryDatabase("todos"));


            this.ConfigureDependencyInjection(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ingenum.Case.Api", Version = "v1" });
            });
        }
    }
}

