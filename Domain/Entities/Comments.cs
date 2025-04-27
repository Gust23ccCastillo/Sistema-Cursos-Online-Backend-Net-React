using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
   
    public class Comments
    {
        [Key]
        public int IdComment { get; set; }

        public string Student {  get; set; }

        [Range(1,6)]
        public int Score { get; set; }

        public string CommentText { get; set; }

        public int IdCourse { get; set; }

        [ForeignKey("IdCourse")]
        public Course Course { get; set; }
    }
}
