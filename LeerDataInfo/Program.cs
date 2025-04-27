using LeerDataInfo.DbContextApplication;
using Microsoft.EntityFrameworkCore;

using (var Db = new AppCourseOnlineDbContext())
{
    var courses = Db.tb_Course.
         Include(includeTablePrice => includeTablePrice.PriceAsigned)
         .Include(includeTableComments => includeTableComments.AsignedComments)
         .Include(IncludeTableProfessor => IncludeTableProfessor.ProfessorLink)
         .ThenInclude(include => include.Professor)
         .AsNoTracking();
      
    foreach (var itemPrint in courses)
    {
        Console.WriteLine("Titulo: "+itemPrint.Title+"\n"+
            "Descripcion: "+itemPrint.Descriptions+"\n"+ 
            "Precio Descuento $:"+itemPrint.PriceAsigned.PromotionalPrice+"\n");

        Console.WriteLine("Profesores: "+"\n");
        foreach (var PrintProfessor in itemPrint.ProfessorLink)
        {
            Console.WriteLine("----" + PrintProfessor.Professor.Name);
        }
        Console.WriteLine("Comentarios: " + "\n");
        foreach (var PrintComments in itemPrint.AsignedComments)
        {
            Console.WriteLine("*******"+PrintComments.CommentText);
        }
    }
};
