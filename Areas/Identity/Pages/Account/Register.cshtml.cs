using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AlausDaryklosGamybosValdymoSistema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AlausDaryklosGamybosValdymoSistema.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Vartotojas> _signInManager;
        private readonly UserManager<Vartotojas> _userManager;
        private readonly RoleManager<Pareigybe> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<Vartotojas> userManager,
            SignInManager<Vartotojas> signInManager,
            RoleManager<Pareigybe> roleManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Prisijungimo vardas privalomas!")]
      [DataType(DataType.Text)]
            [Display(Name = "UserName")]
            public string UserName { get; set;}

            [Required(ErrorMessage = "Vartotojo elektroninis paštas privalomas!")]
            [EmailAddress]
            [DataType(DataType.EmailAddress, ErrorMessage = "elektroninis paštas netinkamo tipo!")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Vartotojo vardas privalomas!")]
            [DataType(DataType.Text)]
            [Display(Name = "Vardas")]
            public string Vardas { get; set; }

            [Required(ErrorMessage = "Vartotojo pavardė privaloma!")]
            [DataType(DataType.Text)]
            [Display(Name = "Pavarde")]
            public string Pavarde { get; set; }

            [Required(ErrorMessage = "Vartotojo rolė privaloma!")]
            [DataType(DataType.Text)]
            [Display(Name = "Role")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Slaptažodis privalomas!")]
            [StringLength(100, ErrorMessage = "{0} turi būti tarp {2} ir {1} simbolių tarpe", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

      [Required(ErrorMessage = "Slaptažodis privalomas!")]
      [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "Slaptažodiai nesutampa!")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new Vartotojas { UserName = Input.UserName, Email = Input.Email, Vardas = Input.Vardas , Pavarde = Input.Pavarde };
        bool rolexists = await _roleManager.RoleExistsAsync(Input.Name);
        if (!rolexists)
        {
          var role = new Pareigybe { Name = Input.Name };
          var result2 = await _roleManager.CreateAsync(role);
        }
        var result = await _userManager.CreateAsync(user, Input.Password);

        if (result.Succeeded)
                {
                    if (rolexists)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                           values: new { userId = user.Id, code = code },
                            protocol: Request.Scheme);
                        
                        await _emailSender.SendEmailAsync(Input.UserName, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                        await _userManager.AddToRoleAsync(user, Input.Name);
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }


            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
