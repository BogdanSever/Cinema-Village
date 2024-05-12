using CinemaVillage.DatabaseContext;
using CinemaVillage.Services.BookingAppService;
using CinemaVillage.Services.BookingAppService.Interface;
using CinemaVillage.Services.DirectorsAppService;
using CinemaVillage.Services.DirectorsAppService.Interface;
using CinemaVillage.Services.HelperService;
using CinemaVillage.Services.HelperService.Interface;
using CinemaVillage.Services.MoviesAppService;
using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.MovieXrefTheatreAppService;
using CinemaVillage.Services.MovieXrefTheatreAppService.Interface;
using CinemaVillage.Services.TheatreAppService;
using CinemaVillage.Services.TheatreAppService.Interface;
using CinemaVillage.Services.UserAppService;
using CinemaVillage.Services.UserAppService.Interface;
using CinemaVillage.ViewModels.Admin.AdminBuilder.AdminFactory;
using CinemaVillage.ViewModels.Admin.AdminBuilder.AdminFactory.Interface;
using CinemaVillage.ViewModels.CheckOut.CheckOutBuilder.CheckOutFactory;
using CinemaVillage.ViewModels.CheckOut.CheckOutBuilder.CheckOutFactory.Interface;
using CinemaVillage.ViewModels.Home.HomeBuilder.HomeFactory;
using CinemaVillage.ViewModels.Home.HomeBuilder.HomeFactory.Interface;
using CinemaVillage.ViewModels.Program.ProgramBuilder.ProgramFactory;
using CinemaVillage.ViewModels.Program.ProgramBuilder.ProgramFactory.Interface;
using CinemaVillage.ViewModels.SeatSelection.SeatSelectionBuilder.SeatSelectionFactory;
using CinemaVillage.ViewModels.SeatSelection.SeatSelectionBuilder.SeatSelectionFactory.Interface;
using CinemaVillage.ViewModels.User.UserBuilder.UserFactory;
using CinemaVillage.ViewModels.User.UserBuilder.UserFactory.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(option =>
                    {
                        option.LoginPath = "/Access/Login";
                        option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    });
            services.AddHttpContextAccessor();
            services.AddTransient<IHomeFactory, HomeFactory>();
            services.AddTransient<IAdminFactory, AdminFactory>();
            services.AddTransient<IMoviesAppService, MoviesAppService>();
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IUserFactory, UserFactory>();
            services.AddTransient<IBookingAppService, BookingAppService>();
            services.AddTransient<IDirectorAppService, DirectorAppService>();
            services.AddTransient<ITheatreAppService, TheatreAppService>();
            services.AddTransient<IJsonCreatorService, JsonCreatorService>();
            services.AddTransient<IMovieXrefTheatreAppService, MovieXrefTheatreAppService>();
            services.AddTransient<IProgramFactory, ProgramFactory>();
            services.AddTransient<ICheckOutFactory, CheckOutFactory>();
            services.AddTransient<ISeatSelectionFactory, SeatSelectionFactory>();
            services.AddControllersWithViews().AddNewtonsoftJson();
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

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
