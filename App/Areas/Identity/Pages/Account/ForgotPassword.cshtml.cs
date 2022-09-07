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

                string HtmlString =
                    $"<!DOCTYPE html>" +
                    $"<html>" +
                    $"<head>" +
                    $"    <meta charset='utf-8'>" +
                    $"    <meta http-equiv='X-UA-Compatible' content='IE=edge'>" +
                    $"    <title>Set Password</title>" +
                    $"    <meta name='viewport' content='width=device-width, initial-scale=1'>" +
                    $"    <link rel='stylesheet' type='text/css' media='screen' href='{callbackUrl.Split("/")[0]}//{callbackUrl.Split("/")[1]}/{callbackUrl.Split("/")[2]}/wwwroot/css/Email.css'>" +
                    $"</head>" +
                    $"<body>" +
                    $"    <header>" +
                    $"        <h2 id='H-Title'>💌 ASP NET Core Email Service</h2>" +
                    $"    </header>" +
                    $"    <section>" +
                    $"        <h2>비밀번호 변경(Set Password)</h2>" +
                    $"        <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' class='btn btn-primary'>이곳을 선택해주세요(Here Click)!</a>" +
                    $"        <div id='Not-Reply'>" +
                    $"            <p>이 메일은 발신 전용이므로 회신하실 수 없습니다.🙏</p>" +
                    $"            <p>You cannot reply to this e-mail because it is for outgoing use only.</p>" +
                    $"        </div>" +
                    $"    </section>" +
                    $"    <hr>" +
                    $"    <footer>" +
                    $"        <p>© ASP Net Core Identitys</p>" +
                    $"    </footer>" +
                    $"</body>" +
                    $"</html>";

                await _emailSender.SendEmailAsync(
                    Input.Email, "Forgot Password", HtmlString);

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
