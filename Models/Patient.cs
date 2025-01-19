using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age{ get; set; }
        [Required]
        public string Diagnosis { get; set; }

        //Connect patient with Appointment
        //public ICollection<Appointment> Appointments { get; set; }
    }
}
