using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentityExample.CustomValidations;
using AspNetCoreIdentityExample.Models.Authentication;
using AspNetCoreIdentityExample.Models.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentityExample
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(_ => _.UseSqlServer(Configuration["ConnectionStrings:SqlServerConnectionString"]));
            services.AddIdentity<AppUser, AppRole>(m =>
            {
                m.Password.RequiredLength = 5;
                m.Password.RequireDigit = false;
                m.Password.RequireLowercase = false;
                m.Password.RequireNonAlphanumeric = false;
                m.Password.RequireUppercase = false;

                m.User.RequireUniqueEmail = true; //Email adreslerini tekilleştiriyoruz.
                m.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+"; //Kullanıcı adında geçerli olan karakterleri belirtiyoruz.
            })
                .AddUserValidator<CustomUserValidation>()
                .AddPasswordValidator<CustomPasswordValidation>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(m =>
            {
                m.LoginPath = new PathString("/User/Login");
                m.Cookie = new CookieBuilder
                {
                    Name = "AspNetCoreIdentityExampleCookie", //Oluşturulacak Cookie'yi isimlendiriyoruz.
                    HttpOnly = false, //Kötü niyetli insanların client-side tarafından Cookie'ye erişmesini engelliyoruz.
                    Expiration = TimeSpan.FromMinutes(2), //Oluşturulacak Cookie'nin vadesini belirliyoruz.
                    SameSite = SameSiteMode.Lax, //Top level navigasyonlara sebep olmayan requestlere Cookie'nin gönderilmemesini belirtiyoruz.
                    SecurePolicy = CookieSecurePolicy.Always //HTTPS üzerinden erişilebilir yapıyoruz.
                };
                m.SlidingExpiration = true; //Expiration süresinin yarısı kadar süre zarfında istekte bulunulursa eğer geri kalan yarısını tekrar sıfırlayarak ilk ayarlanan süreyi tazeleyecektir.
                m.ExpireTimeSpan = TimeSpan.FromMinutes(2); //CookieBuilder nesnesinde tanımlanan Expiration değerinin varsayılan değerlerle ezilme ihtimaline karşın tekrardan Cookie vadesi burada da belirtiliyor.
            });


            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(_ => _.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}"));
        }
    }
}
