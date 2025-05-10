using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DbContextApplication
{
    public class CourseOnlineDbContext : IdentityDbContext<UserApplication>
    {
        public CourseOnlineDbContext(DbContextOptions<CourseOnlineDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //CREAR ARCHIVO DE MIGRACION
            base.OnModelCreating(modelBuilder);

            //AGREGAR LA CONFIGURACION DE VARIAS KEYS EN LA  TABLA DE RELACION MANY TO MANY
            modelBuilder.Entity<Course_Professor>()
                .HasKey(Keys => new { Keys.IdCourse, Keys.IdProfessor });
        }
        public DbSet<Course> tb_Course { get; set; }
        public DbSet<Professor> tb_Professor { get; set; }
        public DbSet<Price> tb_Price { get; set; }
        public DbSet<Comments> tb_Comments { get; set; }

        public DbSet<Course_Professor> tb_Course_Professor { get; set; }

        public DbSet<UserApplication> tb_UserApplication { get; set; }

    }
}
