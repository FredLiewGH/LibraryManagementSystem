using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.EFCore.Entities
{
    [Table("tblbooks")]
    public class Tblbooks
    {
        [Key]
        public Guid BookID { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(200)]
        public string BookName { get; set; } = null!;

        [Required]
        [StringLength(300)]
        public string Author { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Publisher { get; set; } = null!;

        [Required]
        [StringLength(16)]
        public string Status { get; set; } = string.Empty;

        [Column(TypeName = "timestamp")]
        public DateTime CreatedTimestamp { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime UpdatedTimestamp { get; set; }
    }
}
