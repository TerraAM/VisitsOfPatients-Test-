using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Data;
using WebApplication1.App_Start;
using WebApplication1.Models.Entity;

namespace WebApplication1.Controllers
{
	public class VisitsController : Controller
	{
		private readonly dbContext _context;
		public VisitsController(dbContext context)
		{
			_context = context;
		}
		// GET: VisitsController
		public ActionResult Index(int? id)
		{
			if (id == null)
			{
				return View(_context.Visits
					.Include(p=>p.Patient)
					.Include(d=>d.Diagnosis));
			}

			var visits = _context.Visits.Where(x => x.PatientId == id)
				.Include(p=>p.Patient)
                .Include(d => d.Diagnosis);

			if (!visits.Any())
			{
                return View(_context.Visits
					.Include(p => p.Patient)
                    .Include(d => d.Diagnosis));
            }

			return View(visits);
		}

		// GET: VisitsController/Details/5
		//public ActionResult Details(int id)
		//{
		//	return View();
		//}

		// GET: VisitsController/Create
		public ActionResult Create()
		{
            var visit = new Visits();
            ViewData["PatientId"] = new SelectList(_context.Patients.Select(p => new
            {
                Id = p.Id,
                FullName = $"{p.SName} {p.FName} {p.MName}".Trim()
            }).ToList(), "Id", "FullName");
            ViewData["DiagnosisId"] = new SelectList(_context.Diagnoses, "Id", "mkb_name");
            return PartialView("_CreateVisitModelPartial", visit);
        }

		// POST: VisitsController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind("Id, Date, DiagnosisId, PatientId")] Visits visit)
		{
            if (ModelState.IsValid)
            {
                _context.Visits.Add(visit);
                _context.SaveChanges();
            }
            ViewData["PatientId"] = new SelectList(_context.Patients.Select(p => new
            {
                Id = p.Id,
                FullName = $"{p.SName} {p.FName} {p.MName}".Trim()
            }).ToList(), "Id", "FullName");
            ViewData["DiagnosisId"] = new SelectList(_context.Diagnoses, "Id", "mkb_name");

            return PartialView("_CreateVisitModelPartial", visit);
        }

		// GET: VisitsController/Edit/5
		//public ActionResult Edit(int id)
		//{
		//	return View();
		//}

		//// POST: VisitsController/Edit/5
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Edit(int id, IFormCollection collection)
		//{
		//	try
		//	{
		//		return RedirectToAction(nameof(Index));
		//	}
		//	catch
		//	{
		//		return View();
		//	}
		//}

		//// GET: VisitsController/Delete/5
		//public ActionResult Delete(int id)
		//{
		//	return View();
		//}

		//// POST: VisitsController/Delete/5
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Delete(int id, IFormCollection collection)
		//{
		//	try
		//	{
		//		return RedirectToAction(nameof(Index));
		//	}
		//	catch
		//	{
		//		return View();
		//	}
		//}
	}
}
