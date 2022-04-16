using AvadaRestaurantFinal.Models;
using AvadaRestaurantFinal.Services;
using AvadaRestaurantFinal.Services.Interfaces;
using AvadaRestaurantFinal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailServices _emailServices;

        public AccountController(
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager, IEmailServices emailServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailServices = emailServices;
        }


        public IActionResult Register()
        {
            //return RedirectToAction("Index","Home");
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = new AppUser
            {
                FullName = register.FullName,
                UserName = register.UserName,
                Email = register.Email

            };
            user.isActive = true;
            IdentityResult identityResult = await _userManager.CreateAsync(user, register.Password);

            if (!identityResult.Succeeded)
            {

                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, "Member");
            await _signInManager.SignInAsync(user, true);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult CheckSignIn()
        {
            return Content(User.Identity.IsAuthenticated.ToString());
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();
            AppUser dbUser = await _userManager.FindByNameAsync(login.UserName);

            if (dbUser == null)
            {
                ModelState.AddModelError("", "UserName or Password invalid");
                return View();
            }
            if (!dbUser.isActive)
            {
                ModelState.AddModelError("", "user is deactive");
                return View();
            }
            var singInResult = await _signInManager.PasswordSignInAsync(dbUser, login.Password, true, true);



            if (singInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "is lockout");
                return View();
            }
            if (!singInResult.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password invalid");
                return View();
            }

            var roles = await _userManager.GetRolesAsync(dbUser);
            if (roles[0] == "Admin")
            {
                return RedirectToAction("Index", "Dashboard", new { area = "AdminArea" });
            };
            return RedirectToAction("Index", "Home");
        }



        public async Task CreateRole()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            }
            if (!await _roleManager.RoleExistsAsync("Member"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
            }

        }


        public IActionResult Index()
        {
            return View();
        }




        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                ModelState.AddModelError("","This email hasn't been registrated");
                return View(model);
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action(nameof(ResetPassword),"Account",new { email =user.Email,token =code},Request.Scheme,Request.Host.ToString());

            string html = $"<a href={link}>Click here for forgot password</a>";
            string content = "Email for Forgot Password";
            await _emailServices.SendEmailAsync(user.Email,user.UserName,html,content); 
            return RedirectToAction(nameof(ForgetPasswordConrifim));
        }

        public IActionResult ForgetPasswordConrifim()
        {
            return View();
            
        }
        public IActionResult ResetPassword(string email,string token)
        {
            return Ok();
        }








        //public async Task<IActionResult> ResetPassword(string email, string token)
        //{
        //    AppUser user = await _userManager.FindByEmailAsync(email);
        //    if (user == null) return NotFound();
        //    ForgetPassword forgetPassword = new ForgetPassword
        //    {
        //        Token = token,
        //        User = user

        //    };
        //    return View(forgetPassword);
        //}
        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //[ActionName("ResetPassword")]
        //public async Task<IActionResult> Reset(ForgetPassword model)
        //{

        //    AppUser user = await _userManager.FindByEmailAsync(model.User.Email);
        //    if (user == null) return NotFound();
        //    ForgetPassword forgetPassword = new ForgetPassword
        //    {
        //        Token = model.Token,
        //        User = user

        //    };
        //    if (!ModelState.IsValid) return View(forgetPassword);

        //    IdentityResult result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

        //    foreach (var item in result.Errors)
        //    {
        //        ModelState.AddModelError("", item.Description);
        //    }
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
