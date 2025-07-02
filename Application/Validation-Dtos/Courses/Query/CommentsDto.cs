using System.ComponentModel.DataAnnotations;

namespace Application.Automapper.Courses.Query
{
    public class CommentsDto
    {
        public Guid IdComment { get; set; }
        public string Student { get; set; }
        public int Score { get; set; }
        public string CommentText { get; set; }
        public Guid IdCourse { get; set; }
    }
}
