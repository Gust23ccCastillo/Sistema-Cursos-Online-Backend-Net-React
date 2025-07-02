using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Automapper.Courses.Query
{
    public class PriceDto
    {
        public Guid IdPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal? PromotionalPrice { get; set; }
        public Guid IdCourse { get; set; }
    }
}
