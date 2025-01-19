using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Range(10, 1000, ErrorMessage = "Appointment cost must be between 10 and 1000")]
        public int app_price { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        public string? Image { get; set; }

        // Navigation Properties
        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
