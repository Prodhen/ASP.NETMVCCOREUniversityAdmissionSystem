using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1262228_Arosh.Data;
using Microsoft.AspNetCore.Mvc;

namespace _1262228_Arosh.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationDbContext _db;

        public RolesController(ApplicationDbContext db)
        {
            this._db = db;
          
        }

        public IActionResult Index()
        {
            var roles = _db.Roles.ToList();
            return View(roles);
        }
    }
}
