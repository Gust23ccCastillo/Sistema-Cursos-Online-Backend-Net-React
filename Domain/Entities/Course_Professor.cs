using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Course_Professor
    {
       
        public int IdCourse { get; set; }

        [ForeignKey("IdCourse")]
        public Course Course { get; set; }

        public int IdProfessor {  get; set; }

        [ForeignKey("IdProfessor")]
        public Professor Professor { get; set; }
    }
}
