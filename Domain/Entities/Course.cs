using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{

    public class Course
    {
        [Key]
        public int IdCourse {  get; set; }
        
       public string Title { get; set; }

       public string Descriptions { get; set; }

       public DateTime? DatetimePublic { get; set; }

        public string? PhotoName { get; set; }

       public string? UrlPhoto {  get; set; }

        public Price PriceAsigned { get; set; }

        public ICollection<Comments> AsignedComments { get; set; }  
        public ICollection<Course_Professor> ProfessorLink { get; set; } 
    }
}
