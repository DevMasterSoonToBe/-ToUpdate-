using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AlausDaryklosGamybosValdymoSistema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Owin.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net.Mail;

namespace AlausDaryklosGamybosValdymoSistema
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            

      SmtpClient client = new SmtpClient();
      client.Host = "smtp.gmail.com";
      client.Port = 587;
      client.EnableSsl = true;
      client.UseDefaultCredentials = true;
      client.Credentials = new System.Net.NetworkCredential()
      {
        UserName = "alausdaryklaitpagalba@gmail.com",
        Password = "alausdarykla123"
      };
      return client.SendMailAsync("alausdaryklaitpagalba@gmail.com",
                                  email,
                                  subject,
                                  message);
    }
    }
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


/*services.AddDbContext<Alaus_daryklos_gamybos_valdymas_DBContext>(options =>
      options.UseSqlServer("Data Source=DESKTOP-BV01DO0\\DEVSQLSERVER2017;Initial Catalog=Alaus_daryklos_gamybos_valdymas_DB;Integrated Security=True"));
       services.AddDbContext<Alaus_daryklos_gamybos_valdymas_DBContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("TestConnection")));
           */
      services.AddDbContext<Alaus_daryklos_gamybos_valdymas_DBContext>(options =>
      options.UseSqlServer("Data Source=alausdaryklosgamybosorganizavimosistemadbserver.database.windows.net;Initial Catalog=AlausDaryklosGamybosOrganizavimoSistema_db;User ID=sqladmin;Password=wataitau1R;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
      services.AddDbContext<Alaus_daryklos_gamybos_valdymas_DBContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("TestConnection")));

      services.AddIdentity<Vartotojas, Pareigybe>().AddDefaultUI(UIFramework.Bootstrap4).
                AddEntityFrameworkStores<Alaus_daryklos_gamybos_valdymas_DBContext>
                ().AddDefaultTokenProviders();

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
      .AddRazorPagesOptions(options =>
            {
                options.AllowAreas = true;
                options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
            });

      services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
            services.AddSingleton<IEmailSender, EmailSender>();
      services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

    }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
                
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
      app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=KlientoUzsakymoAplankas}/{action=Index}/{id?}");
            });
        }
    }
}
