using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers.ViewModels;
using Microsoft.CSharp;

namespace YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers
{
    [HandleError]
    public class AccountController : Controller
    {
        //[Authorize(Roles = "ADMINISTRATOR")]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/LogOn

        [AllowAnonymous]
        public ActionResult LogOn()
        {
            return ContextDependentView();
        }

        //
        // POST: /Account/JsonLogOn

        [AllowAnonymous]
        [HttpPost]
        public JsonResult JsonLogOn(LogOnModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return Json(new { success = true, redirect = ReturnUrl });
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState() });
        }

        //
        // POST: /Account/LogOn

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return ContextDependentView();
        }

        //
        // POST: /Account/JsonRegister

        [AllowAnonymous]
        [HttpPost]
        public ActionResult JsonRegister(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);
                    return Json(new { success = true });
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState() });
        }

        //
        // POST: /Account/Register

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                //add user to role
                Roles.AddUserToRole(model.UserName, model.Role.RoleName);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        [Authorize]
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        private ActionResult ContextDependentView()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("action");
            if (Request.QueryString["content"] != null)
            {
                ViewBag.FormAction = "Json" + actionName;
                return PartialView();
            }
            else
            {
                ViewBag.FormAction = actionName;
                return View();
            }
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors
                .Select(error => error.ErrorMessage));
        }

        //[Authorize(Roles = "ADMINISTRATOR")]
        public ActionResult Account_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetUsers().ToDataSourceResult(request));
        }

        [HttpPost]
        //[Authorize(Roles = "ADMINISTRATOR")]
        public ActionResult Account_Create([DataSourceRequest] DataSourceRequest request, RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                string defaultPassword = "jayamesin";
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, defaultPassword, model.Email, null, null, true, null, out createStatus);

                //add user to role
                Roles.AddUserToRole(model.UserName, model.Role.RoleName);
            }

            // If we got this far, something failed, redisplay form
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        //[Authorize(Roles = "ADMINISTRATOR")]
        public ActionResult Account_Update([DataSourceRequest] DataSourceRequest request, RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipUser user = Membership.GetUser(model.UserName);
                user.Email = model.Email;

                Membership.UpdateUser(user);

                //remove role
                if (Roles.GetRolesForUser(model.UserName).Length > 0)
                    Roles.RemoveUserFromRoles(model.UserName, Roles.GetRolesForUser(model.UserName));
                //add user to role
                Roles.AddUserToRole(model.UserName, model.Role.RoleName);
            }

            // If we got this far, something failed, redisplay form
            return Json(ModelState.ToDataSourceResult());
        }

        [HttpPost]
        //[Authorize(Roles = "ADMINISTRATOR")]
        public ActionResult Account_Destroy([DataSourceRequest] DataSourceRequest request, RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                //delete user
                Membership.DeleteUser(model.UserName);

                //remove role
                if (Roles.GetRolesForUser(model.UserName).Length > 0)
                    Roles.RemoveUserFromRoles(model.UserName, Roles.GetRolesForUser(model.UserName));
            }

            // If we got this far, something failed, redisplay form
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<RegisterModel> GetUsers()
        {
            MembershipUserCollection users = Membership.GetAllUsers();

            IList<RegisterModel> list = new List<RegisterModel>();
            RegisterModel vm = new RegisterModel();
            foreach (MembershipUser user in users)
            {
                vm = new RegisterModel();
                vm.UserName = user.UserName;
                vm.Email = user.Email;
                vm.Role = ConvertToRoleViewModel(Roles.GetRolesForUser(user.UserName));
                vm.RoleName = ConvertToString(Roles.GetRolesForUser(user.UserName));
                list.Add(vm);
            }
            return list;
        }

        private RoleViewModel ConvertToRoleViewModel(string[] roles)
        {
            RoleViewModel roleVm = new RoleViewModel();
            if (roles.Length > 0)
            {
                roleVm.RoleId = roles[0];
                roleVm.RoleName = roles[0];
            }
            return roleVm;
        }

        private string ConvertToString(string[] roles)
        {
            string result = string.Empty;
            foreach (string s in roles)
            {
                result += s;

            }
            return result;
        }

        public JsonResult GetRoles()
        {
            var listRole = from role in Roles.GetAllRoles()
                           select new RoleViewModel
                           {
                               RoleId = role,
                               RoleName = role
                           };
            return Json(listRole, JsonRequestBehavior.AllowGet);
        }


        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }

    public class RoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
