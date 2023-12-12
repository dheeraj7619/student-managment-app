using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentDetailsInDigitalPlatform.Models;
using StudentDetailsInDigitalPlatform.ViewModels;

namespace StudentDetailsInDigitalPlatform.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IMailService mailService;

        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager,
                                 IMailService mailService   )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mailService = mailService;
        }
        [HttpGet]

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser { Email = model.Email, UserName = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var Token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = Token},Request.Scheme);
                    MailRequest mailRequest = new MailRequest();
                    mailRequest.ToEmail = user.Email;
                    mailRequest.Subject = "Email Confirmation";
                    mailRequest.Body = confirmationLink;

                   await mailService.SendEmailAsync(mailRequest);

                    ViewBag.Title = "Registration Successfull";
                    ViewBag.ErrorMessage = "Before You login Please Confirm Your mail";
                    return View("Error");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    } 
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid )
            {
                var user = await userManager.FindByEmailAsync(model.Email);
               
                if (user != null && !user.EmailConfirmed && 
                (await userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError("", "Email not yet Confirmed");
                    return View(model);
                }
               
                  
                

                var result =await signInManager.PasswordSignInAsync(model.Email, model.Password,
                    model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credential");
                }
            }
            return View();
        }

        
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if(userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }
            var user = await userManager.FindByIdAsync(userId);
            if(user == null)
            {
                ViewBag.ErrorMessage = $"The User Id {userId} is invalid";
                return View("Error");
            }
           var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }
            ViewBag.ErrorMessage = "Email cannot be Confirmed";
            return View("Error");
        }
    }
}
