using MahasDemo.DAL.Contexts;
using MahasDemo.DAL.Data.Model;
using MahasDemo.LL.Interfaces;
using MahasDemo.LL.Repositories;
using MahasDemo.PL.Models.helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

namespace MahasDemo.PL
{
	public class Program
	{
		public static async void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<AppDbContext>(option =>
			{
				option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			builder.Services.AddAutoMapper(m => m.AddProfile(new AppMapperProfile()));
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Option =>
			{
				Option.Password.RequireLowercase = true;
				Option.Password.RequireUppercase = true;
				Option.Password.RequiredLength = 8;
				Option.Password.RequireDigit = true;
				Option.Password.RequiredUniqueChars = 2;
				Option.Password.RequireNonAlphanumeric = true;
				Option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
				Option.User.RequireUniqueEmail = true;
			}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

			builder.Services.ConfigureApplicationCookie(option =>
			{
				option.LoginPath = "/Account/Login";
				option.AccessDeniedPath = "/Home/Error";
				option.LogoutPath = "/Account/LogOut";
				option.ExpireTimeSpan = TimeSpan.FromDays(1);
				option.Cookie.Name = "AhmedHamada";
			});

			var app = builder.Build();

			#region auto Update Database
			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var _dbContext = services.GetRequiredService<AppDbContext>();
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();
			try
			{
				await _dbContext.Database.MigrateAsync();
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "An Error Has Occured While Migrate");
			} 
			#endregion

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
