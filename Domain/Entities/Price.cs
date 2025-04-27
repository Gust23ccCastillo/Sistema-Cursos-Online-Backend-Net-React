using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    
    public class Price
    {
        [Key]
        public int IdPrice { get; set; }

        public decimal CurrentPrice { get; set; }
        public decimal? PromotionalPrice { get; set; }


      
        public int IdCourse { get; set; }

        [ForeignKey("IdCourse")]
        public Course Course { get; set; }
    }
}
