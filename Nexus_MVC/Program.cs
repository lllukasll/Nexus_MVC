using Nexus_MVC.Helpers.XmlSchema;
using Nexus_MVC.Services;
using Nexus_MVC.Services.Interfaces;

namespace Nexus_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration configuration = builder.Configuration;
            builder.Services.Configure<XmlSchemaSettings>(configuration.GetSection("XmlSchemas"));

            builder.Services.AddScoped<IInvoiceService, InvoiceService>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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
