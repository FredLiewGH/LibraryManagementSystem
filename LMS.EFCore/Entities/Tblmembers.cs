using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.EFCore.Entities
{
    [Table("tblmembers")]
    public class Tblmembers
    {
        [Key]
        public Guid MemberID { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string MemberName { get; set; } = null!;

        [Required]
        [StringLength(6)]
        public string Gender { get; set; } = null!;
        
        [Required]
        [StringLength(20)]
        public string PhoneNo { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Email { get; set; } = null!;

        [StringLength(300)]
        public string Address { get; set; } = null!;

        [Column(TypeName = "timestamp")]
        public DateTime CreatedTimestamp { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime UpdatedTimestamp { get; set; }
    }
}
