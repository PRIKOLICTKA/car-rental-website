using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using webcrp10._2.Model;
using webcrp10._2.Services;

namespace webcrp10._2.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var user = _context.Users.SingleOrDefault(u => u.Login == Login && u.Password == Password);
            if (user != null)
            {
                CurrentUser.UserID = user.ID;
                CurrentUser.UserID = user.ID;
                CurrentUser.Login = user.Login;
                CurrentUser.Password = user.Password;
                return RedirectToPage("/index2");
            }
            else
            {
                // Если пользователь не найден, показать ошибку на странице Login, сохраняя введенные данные
                ErrorMessage = "Неправильный логин или пароль";
                return Page();
            }
        }
    }
}

