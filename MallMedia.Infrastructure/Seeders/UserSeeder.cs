﻿using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Infrastructure.Seeders;

public class UserSeeder
{
    public async Task Seed(IServiceProvider serviceProvider)
    {

        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Seed the Admin role
        string roleName = UserRoles.Admin;
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        // Seed the Admin user
        var adminUsername = "admin"; // Change as needed
        var adminPassword = "Password123!"; // Change as needed

        var adminUser = await userManager.FindByNameAsync(adminUsername);
        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = adminUsername,
                Email = null,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, roleName);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }
        }


    }
}