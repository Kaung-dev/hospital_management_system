using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HospitalManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //Dependency Injection
        private readonly HospitalDbContext _context;

        public HomeController(ILogger<HomeController> logger, HospitalDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {   
            var patients = _context.Patients.ToList();
            return View(patients);
        }

        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid) 
            { 
                _context.Patients.Add(patient);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        public IActionResult Edit(int id)
        { 
            var patient = _context.Patients.Find(id);
            if (patient == null) return NotFound();

            return View(patient);   
        }

        [HttpPost]
        public IActionResult Edit(Patient patient) 
        {
            if (ModelState.IsValid)
            { 
                _context.Patients.Update(patient);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        public IActionResult Delete(int id)
        {
            var patient = _context.Patients.Find(id);
            if(patient == null) return NotFound();

            return View(patient);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id) // Fix the typo here
        {
            var patient = _context.Patients.Find(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //TimeTable
        public IActionResult Timetable()
        {
            var timetable = new List<TimetableSlot>
            {
                new TimetableSlot { Time = "09:00am", Monday = "Dance - Ivana Wong", Tuesday = "Yoga - Marta Healy", Wednesday = "Music - Ivana Wong", Thursday = "Dance - Ivana Wong", Friday = "Art - Kate Alley", Saturday = "English - James Smith" },
                new TimetableSlot { Time = "10:00am", Monday = "Music - Ivana Wong", Tuesday = "", Wednesday = "Art - Kate Alley", Thursday = "Yoga - Marta Healy", Friday = "English - James Smith", Saturday = "" },
                new TimetableSlot { Time = "11:00am", Monday = "Break", Tuesday = "Break", Wednesday = "Break", Thursday = "Break", Friday = "Break", Saturday = "Break" },
                new TimetableSlot { Time = "12:00pm", Monday = "", Tuesday = "Art - Kate Alley", Wednesday = "Dance - Ivana Wong", Thursday = "Music - Ivana Wong", Friday = "", Saturday = "Yoga - Marta Healy" },
                new TimetableSlot { Time = "01:00pm", Monday = "English - James Smith", Tuesday = "Music - Ivana Wong", Wednesday = "", Thursday = "English - James Smith", Friday = "Yoga - Marta Healy", Saturday = "Music - Ivana Wong" }
            };

            return View(timetable);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
