using BlazorRolesMgmt.Data;
using BlazorRolesMgmt.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRolesMgmt.Services
{
    public interface IManageUsersService
    {
        Task AddRoleToUser(string UserId, string SelectedUserRole);
        Task RemoveRoleFromUser(string UserId, string SelectedUserRole);
        Task<List<string>> GetUserRoles(string UserId);
        List<string> GetAllRoles();
        List<ApplicationUser> GetAllUsers();
        Task<ApplicationUser> GetUser(string Id);
        Task<string> AddUser(ApplicationUser User);
        Task UpdateUser(ApplicationUser User);
        Task DeleteUser(ApplicationUser User);
        Task<string> GenerateNewPassword(ApplicationUser User);
    }

    public class ManageUsersService : IManageUsersService
    {
        private UserManager<ApplicationUser> UserManager;
        private RoleManager<IdentityRole> RoleManager;

        public ManageUsersService(UserManager<ApplicationUser> _UserManager, RoleManager<IdentityRole> _RoleManager)
        {
            UserManager = _UserManager;
            RoleManager = _RoleManager;
        }

        public async Task AddRoleToUser(string UserId, string SelectedUserRole)
        {
            var user = await UserManager.FindByIdAsync(UserId);
            if (user != null)
            {
                if (SelectedUserRole != "" && SelectedUserRole != null)
                {
                    // Add the user role
                    await UserManager.AddToRoleAsync(user, SelectedUserRole);
                }
            }
        }

        public async Task RemoveRoleFromUser(string UserId, string SelectedUserRole)
        {
            var user = await UserManager.FindByIdAsync(UserId);
            if (user != null)
            {
                if (SelectedUserRole != "" && SelectedUserRole != null)
                {
                    // Delete the user role
                    await UserManager.RemoveFromRoleAsync(user, SelectedUserRole);
                }
            }
        }

        public async Task<List<string>> GetUserRoles(string UserId)
        {
            var user = await UserManager.FindByIdAsync(UserId);
            if (user != null)
            {
                // Get user roles
                var userRoles = await UserManager.GetRolesAsync(user);
                return userRoles.ToList();
            }
            return new List<string>();
        }

        public List<string> GetAllRoles()
        {
            return RoleManager.Roles.Select(m => m.Name).ToList();
        }

        public List<ApplicationUser> GetAllUsers()
        {
            return UserManager.Users.Where(m => m.Email != "admin@biotemp.com").ToList();
        }

        public async Task<ApplicationUser> GetUser(string Id)
        {
            var user = await UserManager.FindByIdAsync(Id);
            return user;
        }

        public async Task<string> AddUser(ApplicationUser User)
        {
            // Create a random password
            var password = PasswordHelper.CreateRandomPassword();
            var result = await UserManager.CreateAsync(User, password);
            if (result.Succeeded)
                return password;
            return null;
        }

        public async Task UpdateUser(ApplicationUser User)
        {
            var user = await UserManager.FindByIdAsync(User.Id);
            await UserManager.UpdateAsync(user);
        }

        public async Task DeleteUser(ApplicationUser User)
        {
            var user = await UserManager.FindByIdAsync(User.Id);
            if (user != null)
            {
                await UserManager.DeleteAsync(user);
            }
        }

        public async Task<string> GenerateNewPassword(ApplicationUser User)
        {
            var password = PasswordHelper.CreateRandomPassword();

            var resetToken = await UserManager.GeneratePasswordResetTokenAsync(User);
            var result = await UserManager.ResetPasswordAsync(User, resetToken, password);
            if (result.Succeeded)
                return password;
            return null;
        }

    }
}
