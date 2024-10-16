using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;

namespace M_TV.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
    
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationUserViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    UserName = registerVM.UserName,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Email = registerVM.Email
                };
                var result = await userManager.CreateAsync(user, registerVM.Password);
                if (result.Succeeded)
                {
                    List<Claim> claims = new();
                    claims.Add(new Claim("FirstName", user.FirstName));
                    claims.Add(new Claim("LastName", user.LastName));
                    await signInManager.SignInWithClaimsAsync(user, false, claims);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        if (error.Description.Contains("Username"))
                        {
                            ModelState.AddModelError("UserName", error.Description);
                            continue;
                        }
                        ModelState.AddModelError("Password", error.Description);
                    }
                    
                }
            }
            return View(registerVM);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(loginVM.Email);
                if(user is not null)
                {
                    var found = await userManager.CheckPasswordAsync(user, loginVM.Password);
                    if (found)
                    {
                        List<Claim> claims = new();
                        claims.Add(new Claim("FirstName", user.FirstName));
                        claims.Add(new Claim("LastName", user.LastName));
                        await signInManager.SignInWithClaimsAsync(user, loginVM.RememberMe, claims);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "UserName or Password is invalid");
            }
            return View(loginVM);
        }
        
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction(nameof(Register));
        }
    }
}
