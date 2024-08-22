using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoomReservationsVM.ViewModels.Account;

namespace RoomReservationsUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(InputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var passCheck = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);            
            if (!passCheck.Succeeded)
            {
                ModelState.AddModelError(nameof(model.Password), "Napačno uporabniško ime oz. geslo");
                return View(model);
            }

            return RedirectToAction("Index", "RoomView");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "RoomView");
        }
    }
}