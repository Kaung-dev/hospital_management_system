using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class CashFlowController : Controller
    {
        private readonly HospitalDbContext _context;

        public CashFlowController(HospitalDbContext context)
        {
            _context = context;
        }

        // Index View: Show cash flow figures
        public IActionResult Index()
        {
            var cashFlow = _context.CashFlows.FirstOrDefault();
            if (cashFlow == null)
            {
                cashFlow = new CashFlow
                {
                    AppointmentsCash = 0,
                    LabCash = 0,
                    PharmacyCash = 0
                };
                _context.CashFlows.Add(cashFlow);
                _context.SaveChanges();
            }
            return View(cashFlow);
        }

        // Update cash flow: For testing purposes
        [HttpPost]
        public IActionResult UpdateCash(string type, decimal amount)
        {
            var cashFlow = _context.CashFlows.FirstOrDefault();
            if (cashFlow == null) return NotFound();

            switch (type.ToLower())
            {
                case "appointments":
                    cashFlow.AppointmentsCash += amount;
                    break;
                case "lab":
                    cashFlow.LabCash += amount;
                    break;
                case "pharmacy":
                    cashFlow.PharmacyCash += amount;
                    break;
                default:
                    return BadRequest("Invalid type specified.");
            }

            _context.CashFlows.Update(cashFlow);
            _context.SaveChanges();

            TempData["success"] = $"{type} cash flow updated!";
            return RedirectToAction("Index");
        }
    }
}
