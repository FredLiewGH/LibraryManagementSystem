using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.EFCore.Entities
{
    [Table("tblborrows")]
    public class Tblborrows
    {
        [Key]
        public Guid BorrowID { get; set; } = Guid.NewGuid();

        [Required]
        public Guid MemberID { get; set; }

        [Required]
        public Guid BookID { get; set; }
     
        public DateOnly BorrowStartDate { get; set; }

        public DateOnly BorrowEndDate { get; set; }
        
        public bool IsReturned { get; set; } = false;

        [Column(TypeName = "timestamp")]
        public DateTime CreatedTimestamp { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime UpdatedTimestamp { get; set; }
    }
}
