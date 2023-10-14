using Demo.DAl.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using Welcome.Helpers;
using Welcome.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Welcome.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
      {
            return View();
      }
      [HttpPost]
      public async Task< IActionResult> Register(RegisterViewModel registerVm)
      {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    FName=registerVm.FName,
                    LName=registerVm.LName,
                    UserName=registerVm.Email.Split('@')[0],
                    Email = registerVm.Email,
                    IsAgree=registerVm.IsAgree

                };
                var result=await _userManager.CreateAsync(user,registerVm.Pass);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View(registerVm);
      }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Login(LoginViewModel loginVm)
        {
            if (ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(loginVm.Email);
                if (user is not null)
                {
                   var flag=await _userManager.CheckPasswordAsync(user, loginVm.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, loginVm.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index","Employee");
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Password Is Not Correct");

                }
                ModelState.AddModelError(string.Empty,"Email Is Not Existed");
               
            }
            return View();
        }
        public new async Task<IActionResult>SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user=await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var token =await _userManager.GeneratePasswordResetTokenAsync(user);
                    var passwordRestLink = Url.Action("ResetPassword", "Account", new { email = user.Email,Token=token }, Request.Scheme);
                    var email = new Email()
                    {
                        Subject = "Rest Password",
                        Body = passwordRestLink,
                        To = user.Email
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckedYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Email Is Not Existed");

            }
            return View(model);
        }
        public IActionResult CheckedYourInbox()
        {
            return View();
        }

    }
}
