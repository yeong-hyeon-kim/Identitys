using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
namespace App.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ChangePasswordModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "현재 비밀번호는 반드시 입력해야합니다.")]
            [DataType(DataType.Password)]
            [Display(Name = "현재 비밀번호")]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = "새 비밀번호는 반드시 입력해야합니다.")]
            [StringLength(100, ErrorMessage = "{0}는 반드시 {2}~{1} 자리 사이로 입력해야합니다.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "새 비밀번호")]
            public string NewPassword { get; set; }

            [Required(ErrorMessage = "비밀번호 확인란은 반드시 입력해야합니다.")]
            [DataType(DataType.Password)]
            [Display(Name = "비밀번호 확인")]
            [Compare("NewPassword", ErrorMessage = "입력한 비밀번호와 일치하지 않습니다.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("사용자가 암호를 성공적으로 변경했습니다.");
            StatusMessage = "비밀번호가 변경되었습니다.";

            return RedirectToPage();
        }
    }
}
