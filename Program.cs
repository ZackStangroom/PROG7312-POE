using PROG7312_POE.Services;
using PROG7312_POE.Services.Interfaces;

namespace PROG7312_POE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            // Register custom services - register by interface
            builder.Services.AddSingleton<IIssueReportRepository, IssueReportRepository>();
            builder.Services.AddScoped<IIssueReportService, IssueReportService>();
            builder.Services.AddSingleton<IEventRepository, EventRepository>();
            builder.Services.AddSingleton<ISearchPatternService, SearchPatternService>();
            builder.Services.AddScoped<ServiceRequestBSTService>();

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
