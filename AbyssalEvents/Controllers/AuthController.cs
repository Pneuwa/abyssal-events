using Abyssal_Events.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Abyssal_Events.Controllers
{
    public class AuthController : Controller
    {
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser
                {
                    UserName = registerViewModel.Username,
                    Email = registerViewModel.Email,
                };

                var identityResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);

                if (identityResult.Succeeded)
                {
                    var roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, "User");
                    if (roleIdentityResult.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                }
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Login(string ReturnUrl)
        {
            var model = new LoginViewModel { 
                ReturnUrl = ReturnUrl,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
				var signInResult = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);
				if (signInResult.Succeeded && signInResult is not null)
				{
					if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
					{
						return Redirect(loginViewModel.ReturnUrl);
					}
					return RedirectToAction("Index", "Home");
				}
			}
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
