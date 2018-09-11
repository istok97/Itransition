using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kufar.ViewModels;
using Kufar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Kufar.Services;

namespace Kufar.Controllers
{
    [ServiceFilter(typeof(LogFilter))]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AdvertisementsController> _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AdvertisementsController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            _logger.LogCritical("nlog is working from a controller");
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                       
                        return Redirect(model.ReturnUrl);
                    }

                    else
                    {
                      
                        return RedirectToAction("Index", "Home");

                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect login and / or password");
                }
            }
        
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("nlog is working from a controller");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year.Value };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogTrace("User is Aut", user.Email);
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}