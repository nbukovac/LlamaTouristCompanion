using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LlamasTouristCompanion.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LlamasTouristCompanion.Controllers
{
    public class Test : Controller
    {

        private TouristDbContext _db;

        public Test(TouristDbContext db)
        {
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            _db.Apartments.ToList();
            return View();
        }
    }
}
