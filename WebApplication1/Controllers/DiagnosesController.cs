using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using WebApplication1.App_Start;
using WebApplication1.Models;
using WebApplication1.Models.Entity;

namespace WebApplication1.Controllers
{
    public class DiagnosesController : Controller
    {
        private readonly dbContext _context;

        public DiagnosesController(dbContext context)
        {
            _context = context;
        }
        // GET: DiagnosesController
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadMore(int pageNumber, int pageSize)
        {
            var diagnoses = await _context.Diagnoses
                                          .OrderBy(d => d.Id)
                                          .Skip((pageNumber - 1) * pageSize)
                                          .Take(pageSize)
                                          .ToListAsync();
            return PartialView("_DiagnosisListPartial", diagnoses);
        }

        public async Task<ActionResult> Update_download()
        {
            using (var package = new ExcelPackage(new FileInfo("G:\\Работа\\Тестовое задание\\WebApplication1\\WebApplication1\\Content\\spr_mkb10.xlsx")))
            {
                var worksheet = package.Workbook.Worksheets.First();
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) 
                {
                    var diagnosis = new Diagnosis
                    {
                        mkb_cod = worksheet.Cells[row, 1].Value.ToString(),
                        mkb_name = worksheet.Cells[row, 2].Value.ToString(),
                        class_id = worksheet.Cells[row, 2].Value.ToString()
                    };

                    await _context.Diagnoses.AddAsync(diagnosis);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: DiagnosesController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: DiagnosesController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: DiagnosesController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
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

        //// GET: DiagnosesController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: DiagnosesController/Edit/5
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

        //// GET: DiagnosesController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: DiagnosesController/Delete/5
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
    }
}
