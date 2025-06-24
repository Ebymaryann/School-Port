using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Student_Management.Models;
using Student_Management.Services;

namespace Student_Management.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly EmailSender _emailSender;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, EmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal if user is not found or not confirmed
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            // ✅ Generate token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // ✅ Generate reset link
            var resetLink = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

            try
            {
                // ✅ Try sending email
                await _emailSender.SendEmailAsync(
                    model.Email,
                    "Reset Your Password",
                    $"<p>Click the link below to reset your password:</p>" +
                    $"<p><a href='{resetLink}'>Reset Password</a></p>"
                );
            }
            catch (Exception ex)
            {
                // ❌ Log the error (optional: use logging instead of Console)
                Console.WriteLine($"EMAIL ERROR: {ex.Message}");

                // Show a friendly message on the page
                ModelState.AddModelError("", "There was an error sending the email. Please try again later.");
                return View(model);
            }

            return RedirectToAction("ForgotPasswordConfirmation");
        }




        public IActionResult ForgotPasswordConfirmation()
        {
            return View(); // Just a success message
        }


        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null) return BadRequest("Invalid password reset link.");
            return View(new ResetPasswordViewModel { Token = token, Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return RedirectToAction("ResetPasswordConfirmation");

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TestEmail()
        {
            await _emailSender.SendEmailAsync("ebytessy87@gmail.com", "Test Email", "This is a test email.");
            return Content("Email sent.");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
