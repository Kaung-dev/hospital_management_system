using HospitalManagementSystem.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementSystem.Models
{
    [Table("nurses")]
    public partial class Nurse
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 characters.")]
        public string Name { get; set; } = null!;

        [Column("gender")]
        [StringLength(10)]
        public string Gender { get; set; } = null!;

        [Required]
        [Column("fk_dept")]
        [DisplayName("Specialization")]
        public int? FkDept { get; set; }

        [Column("age")]
        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60.")]
        public int Age { get; set; }

        [Column("email")]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Column("password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must have at least 6 characters.")]
        public string Password { get; set; } = null!;

        [Column("phone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15, ErrorMessage = "Phone number must not exceed 15 characters.")]
        public string? Phone { get; set; }

        [Column("address")]
        [StringLength(200)]
        public string? Address { get; set; }

        [ForeignKey("FkDept")]
        public virtual Department? FkDeptNavigation { get; set; }
    }
}
