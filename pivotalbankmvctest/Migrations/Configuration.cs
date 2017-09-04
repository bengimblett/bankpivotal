//namespace pivotalbankmvctest.Migrations
//{
//    using pivotalbankmvctest.EF;
//    //PM> Enable-Migrations -ContextTypeName pivotalbankmvctest.EF.SchoolContext
//    using System;
//    using System.Collections.Generic;
//    using System.Data.Entity;
//    using System.Data.Entity.Migrations;
//    using System.Linq;

//    internal sealed class Configuration : DbMigrationsConfiguration<pivotalbankmvctest.EF.SchoolContext>
//    {
//        public Configuration() 
//        {
//            AutomaticMigrationsEnabled = false;
//        }

//        protected override void Seed(pivotalbankmvctest.EF.SchoolContext context)
//        {
  
//            if (context.Students.Count() > 0)
//            {
//                Console.WriteLine("No need to seed");
//                return;
//            }

//            var defaultStudents = new List<Student>();

//            for (int i = 0; i < 5; i++)
//            {
//                context.Students.Add(new Student() { EnrollmentDate = DateTime.Now.AddYears(-i), FirstMidName = $"Firstname {i}", LastName = $"Surname {i}" });
//            }
//            Console.WriteLine("Seeded db with students");
//        }
//    }
//}
