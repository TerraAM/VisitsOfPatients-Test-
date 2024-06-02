using WebApplication1.Models.Entity;

namespace WebApplication1.Models
{
    public class PatientFilterViewModel
    {
        public string SearchTerm { get; set; } = string.Empty;
        public string SName { get; set; } = string.Empty;
        public string FName { get; set; } = string.Empty;
        public string MName { get; set; } = string.Empty;
        public DateTime? BDate { get; set; }
        public string Telephone { get; set; } = string.Empty;
        public IEnumerable<Patient> Patients { get; set; }
    }
}
