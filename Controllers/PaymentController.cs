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
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PaymentController(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _db.Payments.Include(p => p.Student);
            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {

            var payment = await _db.Payments.Include(p => p.Student).FirstOrDefaultAsync(m => m.PaymentID == id);

            return View(payment);
        }


        public IActionResult Create()
        {
            ViewData["StudentID"] = new SelectList(_db.Students, "StudentID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentID,Amount,Transaction,StudentID")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _db.Add(payment);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentID"] = new SelectList(_db.Students, "StudentID", "Name", payment.StudentID);
            return View(payment);
        }


        public async Task<IActionResult> Edit(int? id)
        {

            var payment = await _db.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = new SelectList(_db.Students, "StudentID", "Name", payment.StudentID);
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Payment payment)
        {
            if (id != payment.PaymentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                    _db.Update(payment);
                    await _db.SaveChangesAsync();
               
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentID"] = new SelectList(_db.Students, "StudentID", "Name", payment.StudentID);
            return View(payment);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            var payment = await _db.Payments.Include(p => p.Student).FirstOrDefaultAsync(m => m.PaymentID == id);

            return View(payment);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _db.Payments.FindAsync(id);
            _db.Payments.Remove(payment);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
