using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AlausDaryklosGamybosValdymoSistema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlausDaryklosGamybosValdymoSistema.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<Vartotojas> _userManager;

        public ResetPasswordModel(UserManager<Vartotojas> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Vartotojo elektroninis paštas privalomas!")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "Slaptažodis privalomas!")]
            [StringLength(100, ErrorMessage = "{0} turi būti tarp {2} ir {1} simbolių tarpe", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
      [Required(ErrorMessage = "Slaptažodis privalomas!")]
      [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "Slaptažodžiai nesutampa!")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = code
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
        // Don't reveal that the user does not exist
        return Redirect(@Url.Page("/Account/Login", new { area = "Identity" }));
      }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
        return Redirect(@Url.Page("/Account/Login", new { area = "Identity" }));
      }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
