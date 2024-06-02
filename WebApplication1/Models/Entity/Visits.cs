using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplication1.Models.Entity
{
    public class Visits
    {
        //[MaxLength(36, ErrorMessage = "Превышена допустима длина строки")]
        public int Id { get; set; }
        [Display(Name = "Время посещения")]
        public DateTime Date { get; set; } = DateTime.Today;
        [Display(Name = "Диагноз")]
        public int DiagnosisId { get; set; }
        [Display(Name = "Пациент")]
        public int PatientId { get; set; }
        [Display(Name = "Диагноз1")]
        public Diagnosis? Diagnosis { get; set; }
        [Display(Name = "Пациент1")]
        public Patient? Patient { get; set; }
    }
}
