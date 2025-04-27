using LeerDataInfo.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeerDataInfo.DbContextApplication
{
    public class AppCourseOnlineDbContext: DbContext
    {
        private const string ConnectionString = @"Data Source=DESKTOP-FNSRPA9\SQLEXPRESS;Initial Catalog=Db_Courses_Online;User ID=sa;Password=Ken!@_Cca123-@;Trust Server Certificate=True";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            base.OnConfiguring(optionsBuilder);
             optionsBuilder.UseSqlServer(ConnectionString);
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course_Professor>()
                .HasKey(Keys => new { Keys.IdCourse, Keys.IdProfessor });
        }
        public DbSet<Course> tb_Course { get; set; }
        public DbSet<Price> tb_Price { get; set; }

        public DbSet<Comments> tb_Comments { get; set; }

        public DbSet<Professor> tb_Professor { get; set; }

        public DbSet<Course_Professor> tb_Course_Professor { get; set; }    
    }
}
