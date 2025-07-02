namespace Persistence.Dapper_Configuration.Dtos
{
    public class ProfessorDto
    {
        public Guid ProfessorId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Grade { get; set; }
    }
}
