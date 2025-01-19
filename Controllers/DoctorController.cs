using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalManagementSystem.Controllers
{
    public class DoctorController : Controller
    {
        private readonly HospitalDbContext _context;

        public DoctorController(HospitalDbContext context)
        {
            _context = context;
        }

        // Display all doctors
        public IActionResult Index()
        {
            var doctors = _context.Doctors.Include(d => d.FkDeptNavigation).ToList();
            return View(doctors);
        }

        // GET: Create doctor
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: Create doctor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctor doctor, IFormFile? drImage)
        {
            if (ModelState.IsValid)
            {
                // Password hashing
                var passwordHasher = new PasswordHasher<string>();
                doctor.Password = passwordHasher.HashPassword(null, doctor.Password);

                // Image upload
                if (drImage != null)
                {
                    var imageName = $"{Guid.NewGuid()}_{drImage.FileName}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/dr", imageName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await drImage.CopyToAsync(stream);
                    }
                    doctor.Image = imageName;
                }

                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();
                TempData["success"] = "Doctor added successfully.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            return View(doctor);
        }

        // GET: Edit doctor
        public IActionResult Edit(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null) return NotFound();

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", doctor.FkDept);
            return View(doctor);
        }

        // POST: Edit doctor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Doctors.Update(doctor);
                await _context.SaveChangesAsync();
                TempData["success"] = "Doctor updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", doctor.FkDept);
            return View(doctor);
        }

        // DELETE: Doctor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
                TempData["success"] = "Doctor deleted successfully.";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Single Doctor Details
        public IActionResult SingleDoctor(int id)
        {
            var doctor = _context.Doctors.Include(d => d.FkDeptNavigation).FirstOrDefault(d => d.Id == id);
            if (doctor == null) return NotFound();

            return View(doctor);
        }
    }
}
