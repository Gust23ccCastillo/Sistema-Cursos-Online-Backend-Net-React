namespace Application.Automapper.Courses.Query
{
    public class ProfessorInformationDto
    {
        public Guid IdProfessor { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }
        public string Grade { get; set; }

        public string? PhotoName { get; set; }

        public string? UrlPhoto { get; set; }
    }
}
