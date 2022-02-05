using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using _1262228_Arosh.Models;
using _1262228_Arosh.Data;

using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace _1262228_Arosh.Controllers
{
    public class StudentController : Controller
    {

        private ApplicationDbContext _db;
        private IWebHostEnvironment _hostEnvironment;
        public StudentController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            this._db = db;
            this._hostEnvironment = hostEnvironment;
        }



        public IActionResult Index(int? page, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortingName = sortOrder == "Name" ? "Name_D" : "Name";
            ViewBag.SortingUnit = sortOrder == "Unit" ? "Unit_D" : "Unit";
            ViewBag.SortingGender = sortOrder == "Gender" ? "Gender_D" : "Gender";
            var studentlist = from stu in _db.Students select stu;

            switch (sortOrder)
            {

                case "Name":
                    studentlist = studentlist.OrderBy(s => s.Name);
                    break;
                case "Name_D":
                    studentlist = studentlist.OrderByDescending(s => s.Name);
                    break;
                case "Unit":
                    studentlist = studentlist.OrderBy(s => s.Unit);
                    break;
                case "Unit_D":
                    studentlist = studentlist.OrderByDescending(s => s.Unit);
                    break;
                case "Gender":
                    studentlist = studentlist.OrderBy(s => s.Gender);
                    break;
                case "Gender_D":
                    studentlist = studentlist.OrderByDescending(s => s.Gender);
                    break;
                default:

                    break;
            }



            int pageSize = 3;
            int pageNumber = (page ?? 1);


            return View(studentlist.ToList().ToPagedList(pageNumber, pageSize));
        }



        public IActionResult Create()
        {

            List<AcademicResult> academicResults = new List<AcademicResult>();

            Student student = new Student();

            student.AcademicResults.Add(new AcademicResult() { AcademicResultID = 1 });

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection, Student s)
        {
            try
            {


                string webrootpath = _hostEnvironment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(s.UploadImage.FileName);
                string extension = Path.GetExtension(s.UploadImage.FileName);
                s.Picture = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                fileName = Path.Combine(webrootpath + "/Images", fileName);

                var fileStream = new FileStream(fileName, FileMode.Create);

                s.UploadImage.CopyTo(fileStream);




                _db.Students.Add(s);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {

            Student student = _db.Students.Include(a => a.AcademicResults).Where(r => r.StudentID == id).FirstOrDefault();

            return View(student);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Student s)
        {


            try
            {
                string webrootpath = _hostEnvironment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(s.UploadImage.FileName);
                string extension = Path.GetExtension(s.UploadImage.FileName);
                s.Picture = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
     
                fileName = Path.Combine(webrootpath + "/Images", fileName);

                var fileStream = new FileStream(fileName, FileMode.Create);

                s.UploadImage.CopyTo(fileStream);
                _db.Update(s);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
               return View(s);
            }

        }



        public  IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Student student = _db.Students.Include(a => a.AcademicResults).Where(r => r.StudentID == id).FirstOrDefault();
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _db.Students.FindAsync(id);
            _db.Students.Remove(student);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Details(int id)
        {
            Student student = _db.Students.Include(a=>a.AcademicResults).Where(r=>r.StudentID==id).FirstOrDefault();     
            return View(student);
        }



    }
}
