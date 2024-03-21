using CinemaVillage.DatabaseContext;
using CinemaVillage.Services.MoviesAppService;
using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.ViewModels.Home.HomeBuilder.HomeFactory;
using CinemaVillage.ViewModels.Home.HomeBuilder.HomeFactory.Interface;
using Microsoft.EntityFrameworkCore;

namespace CinemaVillage
{
    public class Startup
    {
        public IConfiguration _configRoot { get; }

        public Startup(IConfiguration configRoot)
        {
            _configRoot = configRoot;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IHomeFactory, HomeFactory>();
            services.AddTransient<IMoviesAppService, MoviesAppService>();
            services.AddControllersWithViews();
            services.AddDbContext<CinemaDbContext>(options => options.UseSqlServer(_configRoot.GetConnectionString("DbContext")));
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
