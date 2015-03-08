using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Text;
    using Model;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<StudentSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(StudentSystemDbContext context)
        {
            context.Students.AddOrUpdate(new Student
            {
                Name = "Student1",
                PhoneNember = "359 000 000",
                Birthday = new DateTime(1950, 1, 1),
                RegistrationDate = new DateTime(2015, 1, 1)
            });

            context.Students.AddOrUpdate(new Student
            {
                Name = "Student2",
                PhoneNember = "359 111 000",
                Birthday = new DateTime(1952, 1, 1),
                RegistrationDate = new DateTime(2015, 1, 1)
            });

            context.Students.AddOrUpdate(new Student
            {
                Name = "Student3",
                PhoneNember = "359 222 000",
                Birthday = new DateTime(1954, 1, 1),
                RegistrationDate = new DateTime(2015, 1, 2)
            });

            context.Students.AddOrUpdate(new Student
            {
                Name = "Student4",
                PhoneNember = "359 222 000",
                Birthday = new DateTime(1956, 1, 1),
                RegistrationDate = new DateTime(2015, 1, 3)
            });

            var resource1 = new Resource
            {
                Name = "Resource 1",
                TypeOfResourse = TypeOfResourse.Video,
                Link = "www.youtube.com"
            };

            context.Resources.AddOrUpdate(resource1);


            var resource2 = new Resource
            {
                Name = "Resource 2",
                TypeOfResourse = TypeOfResourse.Document,
            };

            context.Resources.AddOrUpdate(resource2);


            var course = new Course
            {
                Name = "Course 1",
                Discription = "jgds gldsldjf dgj dgf g ",
                StartDate = new DateTime(2014, 1, 1),
                EndDate = new DateTime(2014, 1, 30),
                Price = 100
            };

            course.Resources.Add(resource1);
            course.Resources.Add(resource2);
            context.Courses.AddOrUpdate(course);

            course = new Course
            {
                Name = "Course 2",
                Discription = "jgds gldsldjf dgj dgf g ",
                StartDate = new DateTime(2015, 2, 1),
                EndDate = new DateTime(2015, 2, 25),
                Price = 200
            };

            context.Courses.AddOrUpdate();
            course.Resources.Add(resource2);
            context.Courses.AddOrUpdate(course);

            context.SaveChanges();

            var student = context.Students.Find(1);
            var course1 = context.Courses.Find(1);

            context.Homeworks.AddOrUpdate(new Homework
            {
                Content = "Homework 1",
                Course = course1,
                Student = student,
                DateAndTime = new DateTime(2015, 3, 30, 23, 59, 59),
            });

            context.SaveChanges();
        }
    }
}
