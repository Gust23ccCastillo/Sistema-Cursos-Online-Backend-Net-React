namespace Application.Automapper.Security.Command
{
    //MODELO A RETORNAR DE INFORMACION DE USUARIO
    public class UserInformationDto
    {
        public string FullName { get; set; }
        public string TokenAccess { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public string UrlImage { get; set; }
    }
}
