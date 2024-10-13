using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webcrp10._2.Model
{
    [Table("АРЕНДА")]
    public class Arenda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Указываем, что ID не автоинкрементируется
        public int ID { get; set; }

        public int ID_user { get; set; } // Указываем, что поле может быть NULL

        public int ID_avto { get; set; } // Указываем, что поле может быть NULL

        public DateTime Дата_Начала_Аренды { get; set; } // Используем DateTime для работы с датами

        public DateTime Дата_Конца_Аренды { get; set; } // Используем DateTime для работы с датами

        public int Цена { get; set; } // Указываем, что поле может быть NULL

        public string? Статус { get; set; }


    }
}
