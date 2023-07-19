﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace ItAcademy.Models
{
    public class Teachers
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz!!")]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz!!")]
        public long PhoneNumber { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz!!")]
        public string Salary { get; set; }
        public bool IsDeactive { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public Positions Positions { get; set; }
        public int PositionsId { get; set; }
        public List<Groups> Groups { get; set; }
        


    }
}
