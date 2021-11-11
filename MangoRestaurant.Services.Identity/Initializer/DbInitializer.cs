using IdentityModel;
using MangoRestaurant.Services.Identity.Data;
using MangoRestaurant.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MangoRestaurant.Services.Identity.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if(_roleManager.FindByNameAsync(SD.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
            }
            else
            {
                return;
            }

            ApplicationUser admin = new()
            {
                UserName = "Admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+9647706242948",
                FirstName = "Saad",
                LastName = "Qais"
            };

            _userManager.CreateAsync(admin, "Me$$i170100!@#").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(admin, SD.Admin).GetAwaiter().GetResult();

            var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, admin.FirstName + " " + admin.LastName),
                new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, SD.Admin),
            }).Result;

            ApplicationUser customer = new()
            {
                UserName = "Customer",
                Email = "customer@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+9647706242948",
                FirstName = "Customer",
                LastName = "Test"
            };

            _userManager.CreateAsync(customer, "Me$$i170100!@#").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(customer, SD.Customer).GetAwaiter().GetResult();

            var customerClaims = _userManager.AddClaimsAsync(customer, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, customer.FirstName + " " + customer.LastName),
                new Claim(JwtClaimTypes.GivenName, customer.FirstName),
                new Claim(JwtClaimTypes.FamilyName, customer.LastName),
                new Claim(JwtClaimTypes.Role, SD.Customer),
            }).Result;
        }
    }
}
