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
    public class ResultController : Controller
    {
        private readonly ApplicationDbContext _db;
            

        public ResultController(ApplicationDbContext context)
        {
            _db = context;
        }


        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _db.Results.Include(r => r.Student);
            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {

            var result = await _db.Results.Include(r => r.Student).FirstOrDefaultAsync(m => m.ResultID == id);
            return View(result);
        }


        public IActionResult Create()
        {
            ViewData["StudentID"] = new SelectList(_db.Students, "StudentID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Result result)
        {
            if (ModelState.IsValid)
            {
                _db.Add(result);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentID"] = new SelectList(_db.Students, "StudentID", "Name", result.StudentID);
            return View(result);
        }


        public async Task<IActionResult> Edit(int? id)
        {

            var result = await _db.Results.FindAsync(id);
            ViewData["StudentID"] = new SelectList(_db.Students, "StudentID", "Name", result.StudentID);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Result result)
        {
            if (id != result.ResultID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
   
                _db.Update(result);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentID"] = new SelectList(_db.Students, "StudentID", "Name", result.StudentID);
            return View(result);
        }


        public async Task<IActionResult> Delete(int? id)
        {

            var result = await _db.Results.Include(r => r.Student).FirstOrDefaultAsync(m => m.ResultID == id);
            return View(result);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _db.Results.FindAsync(id);
            _db.Results.Remove(result);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
