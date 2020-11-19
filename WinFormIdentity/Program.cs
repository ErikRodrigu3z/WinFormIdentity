using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.Identity;
// instalar nuggets
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WinFormIdentity.Models;
using WinFormIdentity.Services;

namespace WinFormIdentity
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = Host.CreateDefaultBuilder()
                            .ConfigureAppConfiguration((context, builder) =>
                            {
                                // Add other configuration files...
                                //builder.AddJsonFile("appsettings.local.json", optional: true);
                            })
                            .ConfigureServices((services) =>
                            {
                                ConfigureServices(services);
                            })
                            .ConfigureLogging(logging =>
                            {
                                // Add other loggers...
                            })
                            .Build();


            var servicess = host.Services;
            var mainForm = servicess.GetRequiredService<fLogin>();
            Application.Run(mainForm);
        }



        private static void ConfigureServices(IServiceCollection services)
        {
            //identity
            services.AddIdentity<AppUsers, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            //Presentation other forms can also be injected here
            services.AddSingleton<fLogin>();

            //services
            services.AddScoped<AuthService>();
            services.AddDbContext<ApplicationDbContext>();

        }



    }
}
