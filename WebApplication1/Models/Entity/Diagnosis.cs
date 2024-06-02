using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplication1.Models.Entity
{
    public class Diagnosis
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Код диагноза")]
        public string mkb_cod { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Название диагноз")]
        public string mkb_name { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Класс диагноза")]
        public string class_id { get; set; } = string.Empty;
    }
}
