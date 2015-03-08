namespace StudentSystem.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public class Student
    {
        private ICollection<Course> courses;
        private ICollection<Homework> homeworks;

        public Student()
        {
            this.courses = new HashSet<Course>();
            this.homeworks = new HashSet<Homework>();
        }

        public int Id { get; set; }
  
        [Required]
        [MaxLength(50)]
        public  string Name { get; set; }

        [MaxLength(20)]
        public  string PhoneNember { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }
        public DateTime Birthday { get; set; }

        public ICollection<Course> Courses
        {
            get { return courses; }
        }

        public ICollection<Homework> Homeworks
        {
            get { return homeworks; }
        } 
    }
}
