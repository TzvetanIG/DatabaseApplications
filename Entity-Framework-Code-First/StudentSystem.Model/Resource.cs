using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Model
{
    public class Resource
    {
        private ICollection<Course> courses;

        public Resource()
        {
            this.courses = new HashSet<Course>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public TypeOfResourse TypeOfResourse { get; set; }

        [MaxLength(250)]
        public string Link { get; set; }

        public ICollection<Course> Courses { get { return courses; } } 
    }
}
