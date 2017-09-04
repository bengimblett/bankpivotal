using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace pivotalbankmvctest.EF
{
    public class SchoolContext : DbContext
    {

        // pass in connection string - this will either be from config or CF env var
        public SchoolContext() : base(Startup.GlobalDbConnectionString)
        {
            // seed
            Database.SetInitializer(new SchoolDBInitializer());
        }

        //public DbSet<Course> Courses { get; set; }
        //public DbSet<Department> Departments { get; set; }
        //public DbSet<Enrollment> Enrollments { get; set; }
        //public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Person> People { get; set; }

    }

    public class SchoolDBInitializer : DropCreateDatabaseAlways<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            IList<Student> defaultStudents = new List<Student>();

            for (int i = 0; i< 5; i++)
            {
                context.Students.Add(new Student() { EnrollmentDate = DateTime.Now.AddYears(-i), FirstMidName = $"Firstname {i}", LastName = $"Surname {i}" });
            }

            base.Seed(context);
        }
    }
}