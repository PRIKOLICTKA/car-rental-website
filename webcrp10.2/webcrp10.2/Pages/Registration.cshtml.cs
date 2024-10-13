using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webcrp10._2.Model;
using System.Linq;
using webcrp10._2.Services;

namespace webcrp10._2.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegistrationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Логин и пароль обязательны.";
                return Page();
            }

            var existingUser = _context.Users.SingleOrDefault(u => u.Login == Login);
            if (existingUser != null)
            {
                ErrorMessage = "Пользователь с таким логином уже существует.";
                return Page();
            }

            var newUser = new User
            {
                Login = Login,
                Password = Password
            };

            _context.Users.Add(newUser);
            try
            {
                _context.SaveChanges();
                SuccessMessage = "Регистрация прошла успешно! Вы можете войти.";
                return RedirectToPage("/index");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка при сохранении данных: {ex.Message}";
                return Page();
            }
        }
    }
}
