using BlazorRolesMgmt.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRolesMgmt.Services
{
    public interface IManageRolesService
    {
        List<IdentityRole> GetAllRoles();
        Task<IdentityRole> GetRole(string Id);
        Task AddRole(string Name);
        Task UpdateRole(IdentityRole role);
        Task DeleteRole(string Id);
    }

    public class ManageRolesService : IManageRolesService
    {
        private UserManager<ApplicationUser> UserManager;
        private RoleManager<IdentityRole> RoleManager;

        public ManageRolesService(UserManager<ApplicationUser> _UserManager, RoleManager<IdentityRole> _RoleManager)
        {
            UserManager = _UserManager;
            RoleManager = _RoleManager;
        }

        public List<IdentityRole> GetAllRoles()
        {
            return RoleManager.Roles.ToList();
        }

        public async Task<IdentityRole> GetRole(string Id)
        {
            return await RoleManager.FindByIdAsync(Id);
        }

        public async Task AddRole(string Name)
        {
            await RoleManager.CreateAsync(new IdentityRole(Name));
        }

        public async Task UpdateRole(IdentityRole role)
        {
            await RoleManager.UpdateAsync(role);
        }

        public async Task DeleteRole(string Id)
        {
            var role = await RoleManager.FindByIdAsync(Id);
            if (role != null)
            {
                await RoleManager.DeleteAsync(role);
            }
        }
    }
}
