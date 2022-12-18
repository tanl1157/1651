using Base.Entity.ResponseModels;
using Base.Entity.ViewModels;
using LibraryManagement.Filters;
using LibraryManagement.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    [RolesAuthorize(Roles = "Admin")]
    public class UserController : BasicAuthorizationController
    {
        ApplicationDbContext context = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public UserController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
            var provider = new DpapiDataProtectionProvider("SampleAppName");
            UserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("SampleTokenName"));
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            var response = new MainResponse<List<ApplicationUserViewModel>>();

            try
            {
                var usersViewModel = new List<ApplicationUserViewModel>();
                var users = UserManager.Users.ToList();
                var roles = context.Roles.ToList();

                if (users != null && users.Count > 0)
                {
                    foreach (var user in users)
                    {
                        var firstRoleID = user.Roles.FirstOrDefault().RoleId;
                        var role = roles.FirstOrDefault(_ => _.Id == firstRoleID);
                        usersViewModel.Add(new ApplicationUserViewModel()
                        {
                            Id = user.Id,
                            Email = user.Email,
                            FullName = user.FullName,
                            RoleId = role.Id,
                            RoleName = role.Name
                        });
                    }

                    response.Data = usersViewModel;
                }
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            var response = new MainResponse<bool>();

            try
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FullName = model.FullName };
                var resultAddUser = await UserManager.CreateAsync(user, model.Password);
                var resultAddRole = await UserManager.AddToRoleAsync(user.Id, model.RoleName);

                response.Data = resultAddUser.Succeeded && resultAddRole.Succeeded;
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

        public ActionResult Update(string id)
        {
            var userViewModel = new ApplicationUserViewModel();
            var user  = UserManager.Users.FirstOrDefault(_ => _.Id == id);
            var firstRoleId = user.Roles.FirstOrDefault().RoleId;
            var role = context.Roles.FirstOrDefault(_ => _.Id == firstRoleId);

            userViewModel.Id = user.Id;
            userViewModel.Email = user.Email;
            userViewModel.FullName = user.FullName;
            userViewModel.RoleId = role.Id;
            userViewModel.RoleName = role.Name;

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ApplicationUserViewModel model)
        {
            var response = new MainResponse<bool>();

            try
            {
                var user = await UserManager.FindByIdAsync(model.Id);
                user.Email = model.Email;
                user.UserName = model.Email;
                user.FullName = model.FullName;

                var resultUpdateUser = await UserManager.UpdateAsync(user);

                if(!String.IsNullOrEmpty(model.Password))
                {
                    var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                    var resetPassResult = await UserManager.ResetPasswordAsync(user.Id, token, model.Password);
                }

                var allRoles = context.Roles.Select(_ => _.Name).ToArray();
                await UserManager.RemoveFromRolesAsync(user.Id, allRoles);
                var resultAddRole = await UserManager.AddToRoleAsync(user.Id, model.RoleName);

                response.Data = resultUpdateUser.Succeeded && resultAddRole.Succeeded;
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var response = new MainResponse<bool>();

            try
            {
                var deleteResult = await UserManager.DeleteAsync(UserManager.FindById(id));
                response.Data = deleteResult.Succeeded;
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

    }
}