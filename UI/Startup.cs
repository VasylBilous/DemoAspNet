using System;
using System.Data.Entity;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using UI.Utils;

[assembly: OwinStartup(typeof(UI.Startup))]

namespace UI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // повідомляємо ідентіті який контекст використовувати
            app.CreatePerOwinContext<DbContext>(() => new ApplicationContext());

            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);

            app.CreatePerOwinContext<AppSignInManager>(AppSignInManager.Create);

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Auth/Login")
            });

            InitUsers();
        }

        private void InitUsers()
        {
            var userStore = new UserStore<User>(new ApplicationContext());
            var userManager = new UserManager<User>(userStore);
            var role = new IdentityRole
            {
                Name = "Admin"
            };

            var roleStore = new RoleStore<IdentityRole>(new ApplicationContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            roleManager.Create(role);

            var user = new User
            {
                UserName = "Bill",
                Email = "bill@gmail.com"
            };
            userManager.Create(user, "Qwerty1!");

            var admin = new User
            {
                UserName = "admin",
                Email = "admin@gmail.com"
            };
            userManager.Create(admin, "Qwerty1!");
            userManager.AddToRole(userManager.FindByName("admin").Id, "Admin");
        }
    }
}
