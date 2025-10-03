using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RDP_NTier_Task.DAL.DATA;
using RDP_NTier_Task.DAL.Models;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.DAL.Seed_Data
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public SeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;

            //update data base if not updated to make the migations not applied
            if (!context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }


        public async Task DataSeedAsync()
        {
            if (!await context.Categories.AnyAsync())
            {
                await context.AddRangeAsync(
                   new Category
                   {
                       categoryName = "Laptops",
                       categoryDescription = "This Is For Laptops !! "

                   },
                    new Category
                    {
                        categoryName = "Computers",
                        categoryDescription = "This Is For Computers !! "
                    },
                    new Category
                    {
                        categoryName = "Microwaves",
                        categoryDescription = "This Is For Microwaves !! "
                    }

                    );
            }
            await context.SaveChangesAsync();

        }

        public async Task IdentitySeedAsync()
        {

            if (!await roleManager.Roles.AnyAsync())
            {
                await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("Customer"));
               
            }

            if (!await userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser
                {
                    UserName = "EhsanBarghouthi", // Important: Identity needs UserName
                    Email = "Ehsan@gmail.com",
                    FullName = "Ehsan Barghouthi"
                    
                };
                var user2 = new ApplicationUser
                {
                    UserName = "EB",
                    Email = "EB@gmail.com",
                    FullName = "E B"
                };
                var user3 = new ApplicationUser
                {
                    UserName = "Ehsan",
                    Email = "E@gmail.com",
                    FullName = "Ehsan"
                };

                await userManager.CreateAsync(user1, "pass@123A");
                await userManager.CreateAsync(user2, "pass@123B");
                await userManager.CreateAsync(user3, "pass@123C");

                // Generate and confirm email for user1
                var token1 = await userManager.GenerateEmailConfirmationTokenAsync(user1);
                await userManager.ConfirmEmailAsync(user1, token1);
                var token2 = await userManager.GenerateEmailConfirmationTokenAsync(user2);
                await userManager.ConfirmEmailAsync(user2, token2);
                var token3 = await userManager.GenerateEmailConfirmationTokenAsync(user3);
                await userManager.ConfirmEmailAsync(user3, token3);


                await userManager.AddToRoleAsync(user1, "SuperAdmin");
                await userManager.AddToRoleAsync(user2, "Admin");
                await userManager.AddToRoleAsync(user3, "Customer");



            }

        }

    }
}

