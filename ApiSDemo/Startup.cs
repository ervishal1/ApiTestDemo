using ApiSDemo.Data;
using ApiSDemo.Repositories.Infrastructure;
using ApiSDemo.Repositories.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSDemo
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
			services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("ApiDemo")));

			services.AddTransient(typeof(IGenericRepo<>), typeof(GenericRepo<>));
			services.AddTransient<IUserRepo, UserRepo>();
			services.AddTransient<IFeedBackRepo, FeedBackRepo>();
			services.AddTransient<IUnitOfWork, UnitOfWork>();

			services.AddDistributedMemoryCache();

			services.AddSession(options =>
			{
				options.Cookie.Name = ".UserAuth.Session";
				options.IdleTimeout = TimeSpan.FromSeconds(10);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});

			services.AddCors();
			//services.AddSession(op =>
			//{
			//	op.Cookie.IsEssential = true;
			//	op.IdleTimeout = TimeSpan.FromMinutes(10);
			//	op.IOTimeout = TimeSpan.FromSeconds(5);
			//});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(op => op.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
			app.UseRouting();
			//app.UseSession();

			app.UseAuthorization();

			app.UseSession();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
