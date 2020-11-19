using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormIdentity.Models;

namespace WinFormIdentity.Services
{
    public class AuthService
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager; 
        private readonly RoleManager<IdentityRole> _roleManager; 
        private readonly ApplicationDbContext _db;

        public AuthService(            
            SignInManager<AppUsers> signInManager,
            UserManager<AppUsers> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
            
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;           
            _db = db;
        }

        #region Getters

        public async Task<AppUsers> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<AppUsers> GetUserById(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion


        #region Login, register, unpdate 

        /// <summary>
        /// Login para usuarios de el panel web
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="rememberMe"></param>
        /// <returns>SignInResult</returns>
        public async Task<SignInResult> Login(string email, string password, bool rememberMe = false)
        {
            try
            {
                return await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);
            }
            catch (Exception ex)
            {
                return SignInResult.Failed;
            }
        }

        /// <summary>
        /// Login para Api, regresa AppUserDto con el Token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>AppUserDto</returns>
        public async Task<bool> Login(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(email);
                var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                //var check = await _userManager.CheckPasswordAsync(user, password);
                if (result.Succeeded)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task SignIn(AppUsers model, bool isPersistent = false)
        {
            try
            {
                await _signInManager.SignInAsync(model, isPersistent: false);
            }
            catch (Exception ex)
            {

            }
        }


        #endregion

        #region Roles

        public async Task<bool> IsInRole(string email, string role)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                return await _userManager.IsInRoleAsync(user, role);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IdentityResult> AddToRoleAsync(string email, string role)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                return await _userManager.AddToRoleAsync(user, role);
            }
            catch (Exception ex)
            {
                IdentityError errors = new IdentityError();
                errors.Code = ex.StackTrace;
                errors.Description = ex.Message;
                return IdentityResult.Failed(errors);
            }

        }

        public async Task<IdentityResult> RemoveFromRoleAsync(string email, string role)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                return await _userManager.RemoveFromRoleAsync(user, role);
            }
            catch (Exception ex)
            {
                IdentityError errors = new IdentityError();
                errors.Code = ex.StackTrace;
                errors.Description = ex.Message;
                return IdentityResult.Failed(errors);
            }

        }

        public async Task<List<IdentityRole>> RoleList()
        {
            try
            {
                return await _roleManager.Roles.ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }

        }


        #endregion


       

        public async Task<bool> IsUserActive(string email)
        {
            try
            {
                var res = await _db.AppUsers.FirstOrDefaultAsync(x => x.Email == email);
                if (res.Activo)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> PasswordValidator(string passwword)
        {
            var passwordValidator = new PasswordValidator<AppUsers>();
            return await passwordValidator.ValidateAsync(_userManager, null, passwword);
        }

        public string PasswordHasher(AppUsers user, string passwod)
        {
            try
            {
                return _userManager.PasswordHasher.HashPassword(user, passwod);
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public async Task<string> PasswordHashAsync(AppUsers model)
        {
            try
            {
                var passwordValidator = new PasswordValidator<AppUsers>();
                var passwordResult = await passwordValidator.ValidateAsync(_userManager, null, model.PasswordRecover);

                if (passwordResult.Succeeded)
                {
                    //hash new password
                    var newPasswordHash = _userManager.PasswordHasher.HashPassword(model, model.PasswordRecover);
                    return newPasswordHash;
                }

                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }




     


     


    }
}
