using System.Linq;

namespace StudentSystem.ConsoleClient
{
    using Data;
    using Data.Migrations;
    using System;
    using System.Data.Entity;
    using Model;
    using System.Data.Entity.Migrations;

    static class StudentSystemProgram
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemDbContext, Configuration>());
            var db = new StudentSystemDbContext();

            //Add new student
            db.Students.AddOrUpdate(new Student
            {
                Name = "Nakov",
                Birthday = new DateTime(1980, 1, 1),
                RegistrationDate = new DateTime(2015, 2, 1)
            });

            // Add new resource
            var resource1 = new Resource
            {
                Name = "Nakov video",
                TypeOfResourse = TypeOfResourse.Video,
                Link = "www.youtube.com"
            };

            var resource2 = new Resource
            {
                Name = "Link",
                Link = "www.dir.bg",
                TypeOfResourse = TypeOfResourse.Other,
            };

            db.Resources.AddOrUpdate(resource1, resource2);

            // Add new courses
            var course1 = new Course
            {
                Name = "C# Intro",
                Discription = "jgds gldsldjf dgj dgf g ",
                StartDate = new DateTime(2014, 1, 1),
                EndDate = new DateTime(2014, 1, 30),
                Price = 180
            };

            var course2 = new Course
            {
                Name = "Seminar 1",
                Discription = "jgds gldsldjf dgj dgf g ",
                StartDate = new DateTime(2015, 2, 1),
                EndDate = new DateTime(2015, 2, 25),
                Price = 0
            };

            course1.Resources.Add(resource1);
            course2.Resources.Add(resource2);
            db.Courses.AddOrUpdate(course1, course2);

            db.SaveChanges();

            // Lists all students and their homework submissions
            Console.WriteLine(" * Lists all students and their homework submissions");

            var students = db.Students
                .Include(s => s.Homeworks)
                .Select(s => new
                {
                    s.Name,
                    s.Homeworks
                });

            foreach (var student in students)
            {
                Console.WriteLine(student.Name);
                var homeworks = student.Homeworks;
                if (homeworks.Count == 0)
                {
                    Console.WriteLine("    no homeworks");
                }
                else
                {
                    foreach (var homework in homeworks)
                    {
                        Console.WriteLine("    - " + homework.DateAndTime);
                    }
                }
            }

            Console.WriteLine();

            // List all course and their resources
            Console.WriteLine(" * List all course and their resources");

            var courses = db.Courses
                .Include(c => c.Resources)
                .Select(c => new
                {
                    c.Name,
                    c.Resources
                });

            foreach (var course in courses)
            {
                Console.WriteLine(course.Name);
                var resourses = course.Resources;
                if (resourses.Count == 0)
                {
                    Console.WriteLine("    no resourses");
                }
                else
                {
                    foreach (var resourse in resourses)
                    {
                        Console.WriteLine("    {0}; {1}",resourse.Name, resourse.Link);
                    }
                }
            }


        }
    }
}
