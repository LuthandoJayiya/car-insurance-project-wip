using LLJ_CarInsuranceMS_ASPNET.Services;
using log4net;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace LLJ_CarInsuranceMS_ASPNET;
public class Program
{
    private static readonly ILog logger = LogManager.GetLogger("Program.main method");

    public static void Main(string[] args)
    {
        logger.Info("ASP.NET Core MVC Started!");

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientApp"));

        string clientBaseUrl = builder.Configuration.GetSection("ClientApp:ClientBaseUrl").Value;

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddHttpClient();

        var app = builder.Build();

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

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapAreaControllerRoute(
            name: "AdminArea",
            areaName: "Admin",
            pattern: "Admin/{controller=Home}/{action=Index}/{id?}");


            endpoints.MapAreaControllerRoute(
            name: "AuthArea",
            areaName: "AuthServices",
            pattern: "AuthServices/{controller=Home}/{action=Index}/{id?}");

            endpoints.MapAreaControllerRoute(
            name: "CustomersArea",
            areaName: "Customers",
            pattern: "Customers/{controller=Home}/{action=Index}/{id?}");

            endpoints.MapAreaControllerRoute(
            name: "DriversArea",
            areaName: "Drivers",
            pattern: "Drivers/{controller=Home}/{action=Index}/{id?}");

           endpoints.MapAreaControllerRoute(
           name: "ClaimSurveyorArea",
           areaName: "ClaimSurveyor",
           pattern: "ClaimSurveyor/{controller=Home}/{action=Index}/{id?}");


            endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();

    }
}