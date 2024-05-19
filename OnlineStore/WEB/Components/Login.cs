using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;
using WEB.ViewModels.LoginViewModels;

namespace WEB.Components
{
    public class Login : ViewComponent
    {

        private readonly SignInManager<ApplicationUser> _signInManager;

        public Login(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IViewComponentResult Invoke()
        {
            var model = new AuthorizedViewModel
            {
                IsSignedIn = _signInManager.IsSignedIn((ClaimsPrincipal)User),
                UserName = User.Identity.Name
            };
            return View(model);
        }
    }
}
