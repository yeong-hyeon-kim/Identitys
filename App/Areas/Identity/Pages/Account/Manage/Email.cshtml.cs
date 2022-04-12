using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace ALIMS_Mobile_Web.Areas.Identity.Pages.Account.Manage
{
    public partial class EmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public EmailModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public string Username { get; set; }

        [Display(Name = "이메일")]
        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "새 이메일")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            Input = new InputModel
            {
                NewEmail = email,
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { userId = userId, email = Input.NewEmail, code = code },
                    protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(
                    Input.NewEmail,
                    "[ALIMS] 이메일 확인",
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
                    $"                  이메일 확인을 위해<br>아래 버튼을 눌러주세요." +
                    $"              </p>" +
                    $"              <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' class='btn btn-primary'>이메일 확인</a>" +
                    $"            </div>" +
                    $"        </div>" +
                    $"    </div>" +
                    $"    <script src='{callbackUrl.Split("/")[0]}//{callbackUrl.Split("/")[1]}/{callbackUrl.Split("/")[2]}/wwwroot/lib/jquery/dist/jquery.min.js'></script>" +
                    $"    <script src='{callbackUrl.Split("/")[0]}//{callbackUrl.Split("/")[1]}/{callbackUrl.Split("/")[2]}/wwwroot/lib/bootstrap/dist/js/bootstrap.bundle.min.js'></script>" +
                    $"</body>" +
                    $"</html>");

                StatusMessage = "확인 이메일이 전송되었습니다. 이메일을 확인해 주세요.";
                return RedirectToPage();
            }

            StatusMessage = "이메일이 변경되지 않았습니다.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "[ALIMS] 이메일 확인",
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
                    $"                  이메일 확인을 위해<br>아래 버튼을 눌러주세요." +
                    $"              </p>" +
                    $"              <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' class='btn btn-primary'>이메일 확인</a>" +
                    $"            </div>" +
                    $"        </div>" +
                    $"    </div>" +
                    $"    <script src='{callbackUrl.Split("/")[0]}//{callbackUrl.Split("/")[1]}/{callbackUrl.Split("/")[2]}/wwwroot/lib/jquery/dist/jquery.min.js'></script>" +
                    $"    <script src='{callbackUrl.Split("/")[0]}//{callbackUrl.Split("/")[1]}/{callbackUrl.Split("/")[2]}/wwwroot/lib/bootstrap/dist/js/bootstrap.bundle.min.js'></script>" +
                    $"</body>" +
                    $"</html>");

            StatusMessage = "확인 이메일이 전송되었습니다. 이메일을 확인해 주세요.";
            return RedirectToPage();
        }
    }
}
