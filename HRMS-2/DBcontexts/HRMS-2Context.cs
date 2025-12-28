using HRMS_2.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS_2.DBcontexts
{
    public class HRMS_2Context :DbContext
    {
        public HRMS_2Context(DbContextOptions<HRMS_2Context>options):base(options)
        {

        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lookup>().HasData(
                new Lookup { Id = 1, MajorCode = 0, MinorCode = 0, Name = "Employees Position" },
                new Lookup { Id = 2, MajorCode = 0, MinorCode = 1, Name = "Developer" },
                new Lookup { Id = 3, MajorCode = 0, MinorCode = 2, Name = "HR" },
                new Lookup { Id = 4, MajorCode = 0, MinorCode = 3, Name = "Manager" },

                new Lookup { Id = 5, MajorCode = 1, MinorCode = 0, Name = "Departments Type" },
                new Lookup { Id = 6, MajorCode = 1, MinorCode = 1, Name = "Finance" },
                new Lookup { Id = 7, MajorCode = 1, MinorCode = 2, Name = "Adminstrative" },
                new Lookup { Id = 8, MajorCode = 1, MinorCode = 3, Name = "Technical" }
                );


            modelBuilder.Entity<User>().HasIndex(x=>x.UserName).IsUnique();

            modelBuilder.Entity<Employee>().HasIndex(x=>x.UserId).IsUnique();

            modelBuilder.Entity<User>().HasData(
                new User {Id = 1 , UserName="Admin" , IsAdmid = true , HashedPassword ="$2a$11$kUuAowumTUPGmbOr6lnAvOcZ.hbUNA7g46FG/DirKWB7ILjJcAAQq"}
                );   
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
