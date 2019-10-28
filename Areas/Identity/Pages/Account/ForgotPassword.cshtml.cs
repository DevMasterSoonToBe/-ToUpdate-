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

namespace AlausDaryklosGamybosValdymoSistema.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<Vartotojas> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<Vartotojas> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Vartotojo elektroninis paštas privalomas!")]
      [DataType(DataType.EmailAddress, ErrorMessage = "elektroninis paštas netinkamo tipo!")]
      [EmailAddress]
      public string Email { get; set; }
        }
    
    public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./Login");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { code },
                    protocol: Request.Scheme);



        await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Pasikeisti slaptažodį",
                    $"Pasikeiskite slaptažodį spausdami šią nuorodą -- <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'></a>.");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
