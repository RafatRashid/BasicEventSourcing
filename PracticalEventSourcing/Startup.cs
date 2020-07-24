using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PracticalEventSourcing.Core;
using PracticalEventSourcing.Core.EventStoreAccessors;
using PracticalEventSourcing.Core.Repositories;
using PracticalEventSourcing.Domain.Commands;

namespace PracticalEventSourcing
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
            services.AddControllers();

            var connString = Configuration.GetConnectionString("AppDbString");
            //services.AddTransient<AppDbContext>(provider =>
            //{
            //    var options = provider.GetService<DbContextOptions<AppDbContext>>();


            //    //var optionBuilder = new DbContextOptionsBuilder<AppDbContext>()
            //    //.UseSqlServer(connString);

            //    return new AppDbContext(options);
            //});
            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(connString);
            });

            services.AddMediatR(
                typeof(Startup).GetTypeInfo().Assembly,
                typeof(AggregateRoot).GetTypeInfo().Assembly,
                typeof(ICommand).GetTypeInfo().Assembly
            );


            services.AddTransient(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddTransient(typeof(IEventRepository), typeof(EventRepository));
            services.AddTransient(typeof(ICommandRepository<>), typeof(CommandRepository<>));


            services.AddSwaggerGen(sa =>
            {
                sa.SwaggerDoc("v1", new OpenApiInfo { Title = "Auction API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });


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
