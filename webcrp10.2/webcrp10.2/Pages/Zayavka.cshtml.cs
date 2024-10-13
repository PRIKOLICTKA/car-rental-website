using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using webcrp10._2.Controllers;
using webcrp10._2.Model;
using webcrp10._2.Services;

namespace webcrp10._2.Pages
{
    public class ZayavkaModel : PageModel
    {
        public int UserID { get; set; }
        private readonly ApplicationDbContext _context;

        public ZayavkaModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int ID_avto { get; set; }
        [BindProperty]
        public int ID_user { get; set; }

        [BindProperty, Required]
        public DateOnly Дата_Начала_Аренды { get; set; }
        [BindProperty, Required]
        public DateOnly Дата_Конца_Аренды { get; set; }
        [BindProperty, Required]
        public TimeSpan Время_Вывоза { get; set; }
        [BindProperty, Required]
        public TimeSpan Время_Возврата { get; set; }
        [BindProperty, Required]
        public string Получение_авто { get; set; }
        [BindProperty, Required]
        public string Сдача_авто { get; set; }
        [BindProperty, Required]
        public string Фамилия { get; set; }
        [BindProperty, Required]
        public string Имя { get; set; }
        [BindProperty, Required]
        public string Отчество { get; set; }
        [BindProperty, Required]
        public string Телефон { get; set; }
        [BindProperty, Required]
        public string Email { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

      

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
            
                var zayavka = new Zayavki
                {
                    ID_avto = ID_avto,
                    ID_user = CurrentUser.UserID, // Это значение нужно как-то получить, возможно, из текущего пользователя
                    Дата_Начала_Аренды = Дата_Начала_Аренды,
                    Дата_Конца_Аренды = Дата_Конца_Аренды,
                    Время_Вывоза = Время_Вывоза,
                    Время_Возврата = Время_Возврата,
                    Получение_авто = Получение_авто,
                    Сдача_авто = Сдача_авто,
                    Фамилия = Фамилия,
                    Имя = Имя,
                    Отчество = Отчество,
                    Телефон = Телефон,
                    Email = Email
                };

                _context.Zayavki.Add(zayavka);
                _context.SaveChanges();
                SuccessMessage = "Заявка успешно отправлена!";
                return RedirectToPage("/index2", new { orderSubmitted = true }); // Редирект на главную страницу после успешного оформления заявки
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка при сохранении данных: {ex.Message}";
                return Page();
            }
        }
    
    }
}

