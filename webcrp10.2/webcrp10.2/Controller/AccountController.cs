using Microsoft.AspNetCore.Mvc;
using System.Linq;
using webcrp10._2.Model;
using webcrp10._2.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace webcrp10._2.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("/Pages/Login.cshtml");
        }

        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Login == login && u.Password == password);
            if (user != null)
            {
                // Сохраняем ID пользователя в сеансе
                HttpContext.Session.SetInt32("UserID", user.ID);

                // Сохраняем логин и пароль в статическом классе
                CurrentUser.UserID = user.ID;
                CurrentUser.Login = user.Login;
                CurrentUser.Password = user.Password;

                // Передача ID пользователя в модель представления index2
                var model = new Index2ViewModel
                {
                    UserID = user.ID
                };

                return RedirectToPage("/index2", model);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Неправильный логин или пароль");
                return View("/Pages/Login.cshtml");
            }
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View("/Pages/Registration.cshtml");
        }

        [HttpPost]
        public IActionResult Registration(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(string.Empty, "Логин и пароль обязательны.");
                return View("/Pages/Registration.cshtml");
            }

            var existingUser = _context.Users.SingleOrDefault(u => u.Login == login);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Пользователь с таким логином уже существует.");
                return View("/Pages/Registration.cshtml");
            }

            var newUser = new User
            {
                Login = login,
                Password = password
            };

            _context.Users.Add(newUser);
            try
            {
                _context.SaveChanges();
                ViewBag.SuccessMessage = "Регистрация прошла успешно! Вы можете войти.";
                return RedirectToPage("/index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ошибка при сохранении данных: {ex.Message}");
                return View("/Pages/Registration.cshtml");
            }
        }

        [HttpGet]
        public IActionResult Zayavka()
        {
            return View("/Pages/Zayavka.cshtml");
        }

        [HttpPost]
        public IActionResult Zayavka(string mark, string model, string start, string end, string timestart, string timeend, string from, string to, string surname, string name, string patronymic, string phone, string email)
        {
            try
            {
                // Получаем ID пользователя из сеанса
                var userID = HttpContext.Session.GetInt32("UserID") ?? 0;

                // Конвертируем model в int
                if (!int.TryParse(model, out int modelId))
                {
                    ModelState.AddModelError(string.Empty, "Неверный формат модели.");
                    return View("/Pages/Zayavka.cshtml");
                }

                // Проверяем, что обязательные поля не пусты и происходит успешный парсинг даты и времени
                if (string.IsNullOrEmpty(mark) || modelId == 0 ||
                    !DateOnly.TryParse(start, out var startDate) ||
                    !DateOnly.TryParse(end, out var endDate) ||
                    !TimeSpan.TryParse(timestart, out var timeStart) ||
                    !TimeSpan.TryParse(timeend, out var timeEnd) ||
                    string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to) ||
                    string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(name) ||
                    string.IsNullOrEmpty(patronymic) || string.IsNullOrEmpty(phone) ||
                    string.IsNullOrEmpty(email))
                {
                    ModelState.AddModelError(string.Empty, "Пожалуйста, заполните все обязательные поля корректно.");
                    return View("/Pages/Zayavka.cshtml");
                }

                // Создаем новый объект заявки
                var zayavka = new Zayavki
                {
                    // Заполняем данные из формы
                    ID_avto = modelId,
                    ID_user = userID, // Используем полученный из сеанса ID пользователя
                    Дата_Начала_Аренды = startDate,
                    Дата_Конца_Аренды = endDate,
                    Время_Вывоза = timeStart,
                    Время_Возврата = timeEnd,
                    Получение_авто = from,
                    Сдача_авто = to,
                    Фамилия = surname,
                    Имя = name,
                    Отчество = patronymic,
                    Телефон = phone,
                    Email = email
                };

                _context.Zayavki.Add(zayavka);
                _context.SaveChanges();

                return RedirectToPage("/index2", new { orderSubmitted = true });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ошибка при сохранении данных: {ex.Message}");
                return View("/Pages/Zayavka.cshtml");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Bronirovania()
        {
            // Получаем ID текущего пользователя
            var userID = HttpContext.Session.GetInt32("UserID") ?? 0;

            // Проверяем, что пользователь авторизован
            if (userID == 0)
            {
                return RedirectToAction("Login");
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
            var bronirovania = await _context.Arenda
                .Where(a => a.ID_user == userID)
                .ToListAsync();

            // Передаем данные в представление
            ViewBag.CarModels = carModels;
            return View(bronirovania);
        }
        [HttpGet]
        public async Task<IActionResult> Zayavki()
        {
            // Получаем ID текущего пользователя
            var userID = HttpContext.Session.GetInt32("UserID") ?? 0;

            // Проверяем, что пользователь авторизован
            if (userID == 0)
            {
                return RedirectToAction("Login");
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

            // Получаем данные заявок для текущего пользователя
            var zayavki = await _context.Zayavki
                .Where(a => a.ID_user == userID)
                .ToListAsync();

            // Передаем данные в представление
            return View(zayavki);
        }
        [HttpGet]
        public IActionResult EditProfile()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Kabinet");
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult EditProfile(string login)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Kabinet");
            }

            user.Login = login;

            _context.SaveChanges();

            // Обновляем данные текущего пользователя
            CurrentUser.Login = user.Login;

            return RedirectToAction("index2", "Home");
        }
    }
}









