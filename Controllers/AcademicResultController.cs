using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1262228_Arosh.Data;
using _1262228_Arosh.Models;

namespace _1262228_Arosh.Controllers
{
    public class AcademicResultController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AcademicResultController(ApplicationDbContext context)
        {
            _db = context;
        }

 
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _db.AcademicResults.Include(a => a.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var academicResult = await _db.AcademicResults.Include(a => a.Student).FirstOrDefaultAsync(m => m.AcademicResultID == id);
            return View(academicResult);
        }


        public IActionResult Create()
        {
            ViewData["StudentID"] = new SelectList(_db.Students, "StudentID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( AcademicResult academicResult)
        {
            if (ModelState.IsValid)
            {
                _db.Add(academicResult);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentID"] = new SelectList(_db.Students, "StudentID", "Name", academicResult.StudentID);
            return View(academicResult);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicResult = await _db.AcademicResults.FindAsync(id);
            if (academicResult == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = new SelectList(_db.Students, "StudentID", "Name", academicResult.StudentID);
            return View(academicResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  AcademicResult academicResult)
        {
            if (id != academicResult.AcademicResultID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {             
                   _db.Update(academicResult);
                    await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentID"] = new SelectList(_db.Students, "StudentID", "Name", academicResult.StudentID);
            return View(academicResult);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            var academicResult = await _db.AcademicResults.Include(a => a.Student).FirstOrDefaultAsync(m => m.AcademicResultID == id);
            if (academicResult == null)
            {
                return NotFound();
            }

            return View(academicResult);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var academicResult = await _db.AcademicResults.FindAsync(id);
            _db.AcademicResults.Remove(academicResult);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
