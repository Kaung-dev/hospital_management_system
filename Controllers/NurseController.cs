using HospitalManagementSystem.Models;
using HospitalManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class NurseController : Controller
    {
        private readonly HospitalDbContext _db;

        public NurseController(HospitalDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var nurses = _db.Nurses.Include(n => n.FkDeptNavigation).ToList();
            return View(nurses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            PopulateDepartments();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Nurse nurse)
        {
            if (ModelState.IsValid)
            {
                var passwordHasher = new PasswordHasher<string>();
                nurse.Password = passwordHasher.HashPassword(null, nurse.Password);

                _db.Nurses.Add(nurse);
                _db.SaveChanges();

                TempData["success"] = "Nurse created successfully!";
                return RedirectToAction(nameof(Index));
            }

            PopulateDepartments();
            TempData["error"] = "Error in creating nurse.";
            return View(nurse);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var nurse = _db.Nurses.Find(id);
            if (nurse == null)
                return NotFound();

            PopulateDepartments();
            return View(nurse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Nurse nurse)
        {
            if (ModelState.IsValid)
            {
                _db.Nurses.Update(nurse);
                _db.SaveChanges();

                TempData["success"] = "Nurse updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            PopulateDepartments();
            TempData["error"] = "Error in updating nurse.";
            return View(nurse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var nurse = _db.Nurses.Find(id);
            if (nurse == null)
                return NotFound();

            _db.Nurses.Remove(nurse);
            _db.SaveChanges();

            TempData["success"] = "Nurse deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDepartments()
        {
            var departments = _db.Departments.ToList();
            ViewBag.Depts = departments.ToDictionary(d => d.Id, d => d.Name);
        }
    }
}
