using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webcrp10._2.Model
{
    [Table("ZAYAVKI")]
    public class Zayavki
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }


        public int ID_avto { get; set; }
        public int ID_user { get; set; }


        [Required]
        public DateOnly Дата_Начала_Аренды { get; set; }
        [Required]
        public DateOnly Дата_Конца_Аренды { get; set; }
        [Required]
        public TimeSpan Время_Вывоза { get; set; }
        [Required]
        public TimeSpan Время_Возврата { get; set; }
        [Required]
        public string Получение_авто { get; set; }
        [Required]
        public string Сдача_авто { get; set; }
        [Required]
        public string Фамилия { get; set; }
        [Required]
        public string Имя { get; set; }
        [Required]
        public string Отчество { get; set; }
        [Required]
        public string Телефон { get; set; }
        [Required]
        public string Email { get; set; }

        public string? Статус { get; set; } // Nullable поле
    }
}

