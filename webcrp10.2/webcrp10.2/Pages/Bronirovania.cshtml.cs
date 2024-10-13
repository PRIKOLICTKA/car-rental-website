using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webcrp10._2.Model;
using webcrp10._2.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace webcrp10._2.Pages
{
    public class BronirovaniaModel : PageModel
    {
     
        private readonly ApplicationDbContext _context;

        public BronirovaniaModel(ApplicationDbContext context)
        {
            _context = context;
            Bronirovania = new List<Arenda>();
        }

        public IList<Arenda> Bronirovania { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Получаем ID текущего пользователя из статического класса
            var userID = CurrentUser.UserID;

            // Проверяем, что пользователь авторизован
            if (userID == 0)
            {
                return RedirectToPage("/Login");
            }

            // Словарь для сопоставления ID авто и названий авто
            var carModels = new Dictionary<int, string>
        {
            { 1105, "Toyota Camry AT" },
            { 1101, "Hyundai Staria 3,5 AT Luxe" },
            { 1103, "Hyundai Creta 2,0 AT" },
            { 1104, "Hyundai Creta 1,6 AT" },
            { 1108, "Hyundai Solaris II AT NEW" },
            { 1109, "Hyundai Solaris II AT" },
            { 1110, "Hyundai Solaris 1,6 AT" },
            { 1111, "Hyundai Solaris 1,6 MT" },
            { 1112, "Hyundai Solaris 1,4 MT" },
            { 1117, "Hyundai Accent МТ" },
            { 1114, "Lada Granta liftback AT NEW" },
            { 1115, "Lada Granta sedan MT NEW" },
            { 1116, "Lada Granta liftback MT" },
            { 1118, "Lada Kalina universal MT" },
            { 1107, "Kia Rio AT NEW" },
            { 1102, "Kia Sportage NEW AT" },
            { 1106, "Kia Rio X-Line AT NEW" },
            { 1113, "Renault Logan 2 MT" }
        };

            // Получаем данные бронирований для текущего пользователя
            Bronirovania = await _context.Arenda
                .Where(a => a.ID_user == userID)
                .ToListAsync();

            // Передаем словарь carModels в ViewData
            ViewData["CarModels"] = carModels;

            return Page();
        }
    }
}
