using System.Collections.Generic;

namespace ItAcademy.Models
{
    public class Groups
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Students> Students { get; set; }
        public bool IsDeactive { get; set; }    
        public Courses Courses { get; set; }
        public int CourseId { get; set; }


    }
}
