using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementSystem.Models
{
    [Table("cash_flows")]
    public class CashFlow
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("appointments_cash")]
        [Range(0, double.MaxValue, ErrorMessage = "Value must be positive")]
        public decimal AppointmentsCash { get; set; }

        [Column("lab_cash")]
        [Range(0, double.MaxValue, ErrorMessage = "Value must be positive")]
        public decimal LabCash { get; set; }

        [Column("pharmacy_cash")]
        [Range(0, double.MaxValue, ErrorMessage = "Value must be positive")]
        public decimal PharmacyCash { get; set; }

        // Optional total (not stored in DB, calculated at runtime)
        [NotMapped]
        public decimal TotalCash => AppointmentsCash + LabCash + PharmacyCash;
    }
}
