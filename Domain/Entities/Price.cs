using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    
    public class Price
    {
        [Key]
        public Guid IdPrice { get; set; }

        [Column(TypeName ="decimal(18,4)")]
        public decimal CurrentPrice { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? PromotionalPrice { get; set; }


      
        public Guid IdCourse { get; set; }

        [ForeignKey("IdCourse")]
        public Course Course { get; set; }
    }
}
