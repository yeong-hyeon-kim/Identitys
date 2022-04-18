using Microsoft.AspNetCore.Authentication;
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
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "이메일은 반드시 입력해야합니다.")]
            [EmailAddress]
            [Display(Name = "이메일")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "비밀번호는 반드시 입력해야합니다.")]
            [StringLength(100, ErrorMessage = "{0}는 반드시 {2}~{1} 자리 사이로 입력해야합니다.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "비밀번호")]
            public string? Password { get; set; }

            [Required(ErrorMessage = "비밀번호는 반드시 입력해야합니다.")]
            [DataType(DataType.Password)]
            [Display(Name = "비밀번호 확인")]
            [Compare("Password", ErrorMessage = "비밀번호가 일치하지 않습니다.")]
            public string? ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string? returnUrl = null)
        {
            if (returnUrl == null)
            {
                returnUrl = $"~/Identity/Account/Register";
            }

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            if (returnUrl == null)
            {
                returnUrl = $"~/Identity/Account/Register";
            }

            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("사용자가 암호를 사용하여 새 계정을 만들었습니다.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "이메일 확인",
                        $"이메일을 확인을 완료하시려면 <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>여기를 눌러주세요.</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    switch (error.Code)
                    {
                        case "PasswordRequiresNonAlphanumeric":
                            error.Description = "비밀번호에는 영숫자가 아닌 문자가 하나 이상 있어야 합니다.";
                            break;
                        case "PasswordRequiresLower":
                            error.Description = "비밀번호에는 소문자가 포함되어야 합니다.";
                            break;
                        case "PasswordRequiresUpper":
                            error.Description = "비밀번호에는 대문자가 포함되어야 합니다.";
                            break;
                        default:
                            break;
                    }
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
