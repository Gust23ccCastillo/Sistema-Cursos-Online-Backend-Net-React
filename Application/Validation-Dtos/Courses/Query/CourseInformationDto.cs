using Domain.Entities;

namespace Application.Automapper.Courses.Query
{
    public class CourseInformationDto
    {
        public Guid IdCourse { get; set; }

        public string Title { get; set; }

        public string Descriptions { get; set; }

        public DateTime? DatetimePublic { get; set; }

        public string? PhotoName { get; set; }

        public string? UrlPhoto { get; set; }

        public ICollection<ProfessorInformationDto> Professors { get; set; }

        public PriceDto Price { get; set; }

        public ICollection<CommentsDto> Comments { get; set; }

    }
}
