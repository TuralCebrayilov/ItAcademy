using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ItAcademy.Models
{
    public class Groups
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz!")]
        public List<Students> Students { get; set; }
        public bool IsDeactive { get; set; }    
        public Courses Courses { get; set; }
        public int CourseId { get; set; }


    }
}
