using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Professor
    {
        [Key]
        public int IdProfessor {  get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }
        public string Grade { get; set; }

        public string? PhotoName { get; set; }

        public string? UrlPhoto {  get; set; }

        public ICollection<Course_Professor> CourseLink { get; set; }   
    }
}
