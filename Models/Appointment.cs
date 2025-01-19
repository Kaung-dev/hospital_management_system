using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementSystem.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required, ForeignKey("Patient")]
        public int PatientId { get; set; }

        [Required, ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [Required]
        public string Day { get; set; }

        [Required]
        public int Status { get; set; } // 0 = Cash, 1 = Online Payment

        // Navigation Properties
        public virtual Patient? Patient { get; set; }
        public virtual Department? Department { get; set; }
    }
}
