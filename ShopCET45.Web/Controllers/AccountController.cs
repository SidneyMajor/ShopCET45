using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopCET45.Web.Helpers;
using ShopCET45.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET45.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;


        public AccountController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }


        public IActionResult Login()
        {
            if(this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);
                if(result.Succeeded)
                {
                    if(this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        //Direção de retorno
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }
                    return this.RedirectToAction("Index", "Home");
                }


            }

            this.ModelState.AddModelError(string.Empty, "Failed to login");
            return this.View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);

                if(user == null)
                {
                    user = new Data.Entities.User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username
                    };

                    var result = await _userHelper.AddUserAsync(user, model.Password);
                    if(result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);

                    }

                    var loginViewModel = new LoginViewModel
                    {
                        Password = model.Password,
                        RememberMe = false,
                        Username = model.Username
                    };

                    var result2 = await _userHelper.LoginAsync(loginViewModel);
                    if(result2.Succeeded)
                    {
                        return this.RedirectToAction("Index", "Home");
                    }

                    this.ModelState.AddModelError(string.Empty, "The user couldn't be login.");
                    return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "The user already existed.");
                return this.View(model);
            }

            return this.View(model);
        }


        public async Task<IActionResult> ChangeUser()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new ChangeUserViewModel();


            if(user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if(user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;

                    var response = await _userHelper.UpdateUserAsync(user);
                    if(response.Succeeded)
                    {
                        ViewBag.UserMessage = "User update!";
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found");
                }
            }

            return View(model);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if(this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if(user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if(result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return this.View(model);
        }

    }
}
