﻿using Microsoft.AspNetCore.Identity;

namespace EPT.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("Member").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = "MEMBER";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }

            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }

            
        }

        public void SeedUsers()
        {
            //if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
            //{
            //    IdentityUser user = new IdentityUser();
            //    user.UserName = "usuario@localhost";
            //    user.Email = "usuario@localhost";
            //    user.NormalizedUserName = "USUARIO@LOCALHOST";
            //    user.NormalizedEmail = "USUARIO@LOCALHOST";
            //    user.EmailConfirmed = true;
            //    user.LockoutEnabled = false;
            //    user.SecurityStamp = Guid.NewGuid().ToString();

            //    IdentityResult result = _userManager.CreateAsync(user, "Poliny123.").Result;

            //    if (result.Succeeded)
            //    {
            //        _userManager.AddToRoleAsync(user, "Member").Wait();
            //    }
            //}

            if (_userManager.FindByEmailAsync("adm@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "Administrador";
                user.Email = "adm@localhost";
                user.NormalizedUserName = "ADM@LOCALHOST";
                user.NormalizedEmail = "ADM@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Poliny123.").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

         
        }
    }
}
