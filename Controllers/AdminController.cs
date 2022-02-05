using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


using _1262228_Arosh.ViewModels;
using _1262228_Arosh.Data;
using _1262228_Arosh.Models;
using X.PagedList;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace _1262228_Arosh.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private IWebHostEnvironment _hostEnvironment;
        public AdminController(ApplicationDbContext db,IWebHostEnvironment hostEnvironment)
        {
            this._db = db;
            this._hostEnvironment = hostEnvironment;

        }
        public IActionResult Index(int? page,string sortOrder)
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


        [HttpGet]
        public IActionResult Create()
        {
            StudentVM svm = new StudentVM();
            svm.AcademicResults.Add(new AcademicResult() {AcademicResultID=1});
            return View(svm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentVM svm)
        {
 
            if (ModelState.IsValid)
            {
                Student s = new Student()
                {
                    Name = svm.Name,
                    FatherName = svm.FatherName,
                    MotherName = svm.MotherName,
                    Email = svm.Email,
                    DOB = svm.DOB,
                    Mobile = svm.Mobile,
                    Gender = svm.Gender,
                    Picture = svm.Picture,
                    UploadImage = svm.UploadImage,
                    Unit = svm.Unit,
                    BirthID = svm.BirthID,
                    BloodGroup = svm.BloodGroup,
                    Status = svm.Status,
                    AcademicResults = svm.AcademicResults




                };




                string webrootpath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(s.UploadImage.FileName);
                string extension = Path.GetExtension(s.UploadImage.FileName);
                s.Picture = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                var file = new FileStream(fileName,FileMode.Create);

                s.UploadImage.CopyTo(file);
                _db.Students.Add(s);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));


            }
            return View();

        }

        public IActionResult Details(int id)
        {
            Student s = _db.Students.Include(a => a.AcademicResults).Where(r => r.StudentID == id).FirstOrDefault();

            StudentVM A = new StudentVM()
            {
            Status = s.Status,
            Name = s.Name,
            FatherName = s.FatherName,
            MotherName = s.MotherName,
            Mobile = s.Mobile,
            Picture = s.Picture,
            UploadImage = s.UploadImage,
            Unit = s.Unit,
            AcademicResults = s.AcademicResults,
            BirthID = s.BirthID,
            BloodGroup = s.BloodGroup,
            Gender = s.Gender,
            DOB = s.DOB,
            Email = s.Email
        };
            return View(s);
        }

        public IActionResult Delete(int? id)
        {
            Student s = _db.Students.Include(a => a.AcademicResults).Where(r => r.StudentID == id).FirstOrDefault();

            StudentVM svm = new StudentVM()
            {
                Status = s.Status,
                Name = s.Name,
                FatherName = s.FatherName,
                MotherName = s.MotherName,
                Mobile = s.Mobile,
                Picture = s.Picture,
                UploadImage = s.UploadImage,
                Unit = s.Unit,
                AcademicResults = s.AcademicResults,
                BirthID = s.BirthID,
                BloodGroup = s.BloodGroup,
                Gender = s.Gender,
                DOB = s.DOB,
                Email = s.Email
            };

            return View(svm);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var s = await _db.Students.FindAsync(id);
            _db.Students.Remove(s);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        public ActionResult Edit(int id)
        {
            Student s = _db.Students.Include(a=>a.AcademicResults).Where(b=>b.StudentID==id).FirstOrDefault();

            StudentVM svm = new StudentVM()
            {
                Status = s.Status,
                Name = s.Name,
                FatherName = s.FatherName,
                MotherName = s.MotherName,
                Mobile = s.Mobile,
                Picture = s.Picture,
                UploadImage = s.UploadImage,
                Unit = s.Unit,
                AcademicResults = s.AcademicResults,
                BirthID = s.BirthID,
                BloodGroup = s.BloodGroup,
                Gender = s.Gender,
                DOB = s.DOB,
                Email = s.Email
            };

            return View(svm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StudentVM A, int id)
        {
            

            Student s = _db.Students.Find(id);

            

            s.Name = A.Name;
            s.FatherName = A.FatherName;
            s.MotherName = A.MotherName;
            s.Mobile = A.Mobile;
            s.Picture = A.Picture;
            s.UploadImage = A.UploadImage;
            s.Unit = A.Unit;
            s.AcademicResults = A.AcademicResults;
            s.BirthID = A.BirthID;
            s.BloodGroup = A.BloodGroup;
            s.Gender = A.Gender;
            s.DOB = A.DOB;
            s.Email = A.Email;
            //s.ConfirmMobile = A.ConfirmMobile;
            if (ModelState.IsValid)
            {






                if (s.UploadImage != null)
                {
                    string webrootpath = _hostEnvironment.WebRootPath;

                    string fileName = Path.GetFileNameWithoutExtension(s.UploadImage.FileName);
                    string extension = Path.GetExtension(s.UploadImage.FileName);
                    s.Picture = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
  
                    fileName = Path.Combine(webrootpath + "/Images", fileName);

                    var file = new FileStream(fileName, FileMode.Create);


                    s.UploadImage.CopyTo(file);

                }
                //s.Picture = Session["Image"].ToString();

                _db.Entry(s).State = EntityState.Modified;
                foreach (var ac in A.AcademicResults)
                {
                    _db.Entry(ac).State = EntityState.Modified;
                }

                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(s);

        }





    }
}
