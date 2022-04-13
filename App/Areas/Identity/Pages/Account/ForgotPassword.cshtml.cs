using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace App.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "이메일은 반드시 입력해야합니다.")]
            [Display(Name = "이메일")]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "[ALIMS] 비밀번호 재설정",
                    $"<!DOCTYPE html>" +
                    $"<head>" +
                    $"    <meta charset='utf-8' />" +
                    $"    <meta name='viewport' content='width=device-width, initial-scale=1.0' />" +
                    $"    <link rel='stylesheet' href='{callbackUrl.Split("/")[0]}//{callbackUrl.Split("/")[1]}/{callbackUrl.Split("/")[2]}/lib/bootstrap/dist/css/bootstrap.min.css' />" +
                    $"</head>" +
                    $"<body>" +
                    $"    <div class='bg-light border rounded-3' style='margin:15px;'>" +
                    $"        <div class='card text-center'>" +
                    $"            <div class='card-body'>" +
                    $"              <h3 class='card-title'>" +
                    $"                <b>ALIMS</b>" +
                    $"              </h3>" +
                    $"              <p class='card-text'>" +
                    $"                  비밀번호를 재설정 하시려면<br>아래 버튼을 눌러주세요." +
                    $"              </p>" +
                    $"              <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' class='btn btn-primary'>비밀번호 재설정</a>" +
                    $"            </div>" +
                    $"        </div>" +
                    $"    </div>" +
                    $"    <script src='{callbackUrl.Split("/")[0]}//{callbackUrl.Split("/")[1]}/{callbackUrl.Split("/")[2]}/wwwroot/lib/jquery/dist/jquery.min.js'></script>" +
                    $"    <script src='{callbackUrl.Split("/")[0]}//{callbackUrl.Split("/")[1]}/{callbackUrl.Split("/")[2]}/wwwroot/lib/bootstrap/dist/js/bootstrap.bundle.min.js'></script>" +
                    $"</body>" +
                    $"</html>");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
