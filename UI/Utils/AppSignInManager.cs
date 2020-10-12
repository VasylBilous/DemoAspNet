using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Utils
{
    public class AppSignInManager:SignInManager<User, string>
    {
        public AppSignInManager(AppUserManager userManager, IAuthenticationManager authenticationManager): base(userManager, authenticationManager)
        {
        }
        public static AppSignInManager Create(IdentityFactoryOptions<AppSignInManager> options, IOwinContext owinContext)
        {
            var manager = owinContext.GetUserManager<AppUserManager>(); // create callback
            var signManager = new AppSignInManager(manager, owinContext.Authentication);
            return signManager;
        }
    }
}