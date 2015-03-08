using System;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Model
{
    public class Homework
    {
        public int Id { get; set; }

        [MaxLength(300)]
        public string Content { get; set; }

        [Required]
        public ContentType ContentType { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        public  int StudentId { get; set; }
        public Student Student { get; set; }
        public  int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
