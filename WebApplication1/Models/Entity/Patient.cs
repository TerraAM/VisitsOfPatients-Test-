using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplication1.Models.Entity
{
    public class Patient
    {
        //[MaxLength(36, ErrorMessage = "Превышена допустима длина строки")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        [MaxLength(50, ErrorMessage = "Превышена допустима длина строки")]
        [RegularExpression(@"^[A-Za-zА-Яа-яЁё]+$", ErrorMessage = "Имя может содержать только буквы")]
        public string SName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Имя")]
        [MaxLength(50, ErrorMessage = "Превышена допустима длина строки")]
        [RegularExpression(@"^[A-Za-zА-Яа-яЁё]+$", ErrorMessage = "Имя может содержать только буквы")]
        public string FName { get; set; } = string.Empty;
        [Display(Name = "Отчество")]
        [MaxLength(50, ErrorMessage = "Превышена допустима длина строки")]
        [RegularExpression(@"^[A-Za-zА-Яа-яЁё]+$", ErrorMessage = "Имя может содержать только буквы")]
        public string MName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "День рождения")]
        public DateTime BDate { get; set; } = DateTime.Today;
        [Required]
        [Display(Name = "Телефон")]
        [MaxLength(15, ErrorMessage = "Превышена допустима длина строки")]
        [RegularExpression(@"^8 \d{3} \d{3} \d{2} \d{2}$", ErrorMessage = "Формат телефона должен быть 8 000 000 00 00")]
        public string Telephone { get; set; } = string.Empty;
    }
}
