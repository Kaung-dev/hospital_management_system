using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Specialization { get; set; }

        [Required]
        public int Experience { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }

        public string Address { get; set; }

        public string PersonalProfile { get; set; }

        public string Image { get; set; }

        // Foreign Key
        public int FkDept { get; set; }
        public Department FkDeptNavigation { get; set; }
    }
}
