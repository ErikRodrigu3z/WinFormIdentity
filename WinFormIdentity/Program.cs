using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WinFormIdentity.Models;
using WinFormIdentity.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                            .ConfigureServices((services) =>
                            {
                                ConfigureServices(services);
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
