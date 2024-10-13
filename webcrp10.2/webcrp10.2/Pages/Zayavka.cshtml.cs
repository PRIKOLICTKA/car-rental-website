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
        public DateOnly ����_������_������ { get; set; }
        [BindProperty, Required]
        public DateOnly ����_�����_������ { get; set; }
        [BindProperty, Required]
        public TimeSpan �����_������ { get; set; }
        [BindProperty, Required]
        public TimeSpan �����_�������� { get; set; }
        [BindProperty, Required]
        public string ���������_���� { get; set; }
        [BindProperty, Required]
        public string �����_���� { get; set; }
        [BindProperty, Required]
        public string ������� { get; set; }
        [BindProperty, Required]
        public string ��� { get; set; }
        [BindProperty, Required]
        public string �������� { get; set; }
        [BindProperty, Required]
        public string ������� { get; set; }
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
                    ID_user = CurrentUser.UserID, // ��� �������� ����� ���-�� ��������, ��������, �� �������� ������������
                    ����_������_������ = ����_������_������,
                    ����_�����_������ = ����_�����_������,
                    �����_������ = �����_������,
                    �����_�������� = �����_��������,
                    ���������_���� = ���������_����,
                    �����_���� = �����_����,
                    ������� = �������,
                    ��� = ���,
                    �������� = ��������,
                    ������� = �������,
                    Email = Email
                };

                _context.Zayavki.Add(zayavka);
                _context.SaveChanges();
                SuccessMessage = "������ ������� ����������!";
                return RedirectToPage("/index2", new { orderSubmitted = true }); // �������� �� ������� �������� ����� ��������� ���������� ������
            }
            catch (Exception ex)
            {
                ErrorMessage = $"������ ��� ���������� ������: {ex.Message}";
                return Page();
            }
        }
    
    }
}

