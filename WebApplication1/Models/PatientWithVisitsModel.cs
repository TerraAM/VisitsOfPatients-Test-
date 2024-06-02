using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using WebApplication1.Models.Entity;

namespace WebApplication1.Models
{
    public class PatientWithVisitsModel
    {
        public int Id { get; set; }
        [Display(Name = "Фамилия")]
        public string SName { get; set; } = string.Empty;
        [Display(Name = "Имя")]
        public string FName { get; set; } = string.Empty;
        [Display(Name = "Отчество")]
        public string MName { get; set; } = string.Empty;
        [Display(Name = "День рождения")]
        public DateTime BDate { get; set; } = DateTime.Today;
        [Display(Name = "Телефон")]
        public string Telephone { get; set; } = string.Empty;
        public ICollection<Visits> Visits { get; set; }
    }
}
