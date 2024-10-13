using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webcrp10._2.Services;

namespace webcrp10._2.Pages
{
    public class KabinetModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public KabinetModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return RedirectToPage("/Kabinet");
            }

            Login = user.Login;
            Password = user.Password;

            return Page();
        }

        public IActionResult OnPost()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return RedirectToPage("/Kabinet");
            }

            user.Login = Login;

            _context.SaveChanges();

            // Обновляем данные текущего пользователя
            CurrentUser.Login = user.Login;

            return RedirectToPage("/index2");
        }
    }

}
