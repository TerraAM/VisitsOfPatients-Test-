using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebApplication1.App_Start;
using WebApplication1.Models;
using WebApplication1.Models.Entity;

namespace WebApplication1.Controllers
{
    public class PatientsController : Controller
    {
        private readonly dbContext _context;
        public PatientsController(dbContext context)
        {
            _context = context;
        }
        // GET: PatientsController
        public async Task<IActionResult> Index(string searchString, string sortOrder, DateTime? startDate, DateTime? endDate)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["StartDate"] = startDate?.ToString("yyyy-MM-dd");
            ViewData["EndDate"] = endDate?.ToString("yyyy-MM-dd");
            ViewData["SNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "sname_desc" : "";
            ViewData["FNameSortParm"] = sortOrder == "FName" ? "fname_desc" : "FName";
            ViewData["MNameSortParm"] = sortOrder == "MName" ? "mname_desc" : "MName";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["TelephoneSortParm"] = sortOrder == "Telephone" ? "telephone_desc" : "Telephone";

            var patients = _context.Patients.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(p => p.SName.Contains(searchString)
                                            || p.FName.Contains(searchString)
                                            || p.MName.Contains(searchString));
            }

            if (startDate.HasValue)
            {
                patients = patients.Where(p => p.BDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                patients = patients.Where(p => p.BDate <= endDate.Value);
            }

            switch (sortOrder)
            {
                case "sname_desc":
                    patients = patients.OrderByDescending(p => p.SName);
                    break;
                case "FName":
                    patients = patients.OrderBy(p => p.FName);
                    break;
                case "fname_desc":
                    patients = patients.OrderByDescending(p => p.FName);
                    break;
                case "MName":
                    patients = patients.OrderBy(p => p.MName);
                    break;
                case "mname_desc":
                    patients = patients.OrderByDescending(p => p.MName);
                    break;
                case "Date":
                    patients = patients.OrderBy(p => p.BDate);
                    break;
                case "date_desc":
                    patients = patients.OrderByDescending(p => p.BDate);
                    break;
                case "Telephone":
                    patients = patients.OrderBy(p => p.Telephone);
                    break;
                case "telephone_desc":
                    patients = patients.OrderByDescending(p => p.Telephone);
                    break;
                default:
                    patients = patients.OrderBy(p => p.SName);
                    break;
            }

            return View(await patients.AsNoTracking().ToListAsync());
        }

        // GET: PatientsController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //GET: PatientsController/Create
        public ActionResult Create()
        {
            var patient = new Patient();
            return PartialView("_CreatePatientModelPartial", patient);
        }

        // POST: PatientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id","SName", "FName", "MName", "BDate","Telephone")] Patient patient)
        {

            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
            }
            return PartialView("_CreatePatientModelPartial", patient);
        }

        // GET: PatientsController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: PatientsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PatientsController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: PatientsController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public async Task<IActionResult> ExportToXml(int id)
        {
            var patient = _context.Patients
            .Where(p => p.Id == id)
            .Select(p => new
            {
                p.Id,
                p.SName,
                p.FName,
                p.MName,
                p.BDate,
                p.Telephone
            }).FirstOrDefault();

            if (patient == null)
            {
                return NotFound();
            }

            var visits = _context.Visits
                .Where(v => v.PatientId == id)
                .Select(v => new
                {
                    v.Id,
                    v.Date,
                    v.DiagnosisId,
                    DiagnosisName = _context.Diagnoses
                        .Where(d => d.Id == v.DiagnosisId)
                        .Select(d => d.mkb_name)
                        .FirstOrDefault()
                }).ToList();

            var xDocument = new XDocument(
                new XElement("Patient",
                    new XElement("Id", patient.Id),
                    new XElement("SName", patient.SName),
                    new XElement("FName", patient.FName),
                    new XElement("MName", patient.MName),
                    new XElement("BDate", patient.BDate),
                    new XElement("Telephone", patient.Telephone),
                    new XElement("Visits",
                        visits.Select(v => new XElement("Visit",
                            new XElement("Id", v.Id),
                            new XElement("Date", v.Date),
                            new XElement("DiagnosisId", v.DiagnosisId),
                            new XElement("DiagnosisName", v.DiagnosisName)
                        ))
                    )
                )
            );

            var xmlString = xDocument.ToString();
            var bytes = System.Text.Encoding.UTF8.GetBytes(xmlString);
            return File(bytes, "application/xml", $"Patient_({patient.SName} {patient.FName} {patient.MName})_Visits.xml");
        }
    }
}
