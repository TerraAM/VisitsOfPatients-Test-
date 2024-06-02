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
        public string SName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Имя")]
        [MaxLength(50, ErrorMessage = "Превышена допустима длина строки")]
        public string FName { get; set; } = string.Empty;
        [Display(Name = "Отчество")]
        [MaxLength(50, ErrorMessage = "Превышена допустима длина строки")]
        public string MName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "День рождения")]
        public DateTime BDate { get; set; } = DateTime.Today;
        [Required]
        [Display(Name = "Телефон")]
        [MaxLength(15, ErrorMessage = "Превышена допустима длина строки")]
        public string Telephone { get; set; } = string.Empty;
    }
}
