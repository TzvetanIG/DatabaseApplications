using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Model
{
    using System;

    public class Course
    {
        private ICollection<Homework> homeworks;
        private ICollection<Resource> resources;
        private ICollection<Student> students;

        public Course()
        {
            this.homeworks = new HashSet<Homework>();
            this.resources = new HashSet<Resource>();
            this.students = new HashSet<Student>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Discription { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public double Price { get; set; }

        public ICollection<Student> Students
        {
            get { return students; }
        }

        public ICollection<Resource> Resources
        {
            get { return resources; }
        }

        public ICollection<Homework> Homeworks
        {
            get { return homeworks; }
        }
    }
}
