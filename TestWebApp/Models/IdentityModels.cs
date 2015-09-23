using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TestWebApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int,CustomUserLogin,CustomUserRole,CustomUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser,int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }

    public class CustomUserLogin : IdentityUserLogin<int> { }


    public class CustomRole : IdentityRole<Int32, CustomUserRole>
    {
        public CustomRole() { }

        public CustomRole(string name)
        {
            Name = name;
        }
    }

    public class CustomUserStore :UserStore<ApplicationUser, CustomRole, Int32, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
       public CustomUserStore(ApplicationDbContext context)
            :base(context){ }   
    }

    public class CustomRoleStore : RoleStore<CustomRole, Int32, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context) : base(context)
        {
            
        }
    }
        public class ApplicationDbContext : IdentityDbContext<ApplicationUser,CustomRole,Int32,CustomUserLogin,CustomUserRole,CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}