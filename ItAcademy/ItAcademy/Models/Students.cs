using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace ItAcademy.Models
{
    public class Students
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz!")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz!")]
        public int? Mobil { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz!")]
        //public string Cours { get; set; }
        //[Required(ErrorMessage = "Bu xana boş ola bilməz!")]

        public DateTime? Birthday { get; set; }
        

        public string GetFormattedBirthday()
        {
            // "ToShortDateString()" metodu, tarihi kısa tarih biçiminde döndürür (örn. "20.07.1993")
            return Birthday?.ToShortDateString();
        }

        [NotMapped]
        public IFormFile Photo { get; set; }

        public bool IsDeactive { get; set; }
        public Courses Courses { get; set; }
        public int CoursesId { get; set; }
        public Groups Groups { get; set; }
        public int GroupsId { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz!!")]
        public int Payment { get; set; }
        

    }
}
