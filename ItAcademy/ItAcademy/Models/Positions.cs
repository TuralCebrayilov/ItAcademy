﻿using System.Collections.Generic;

namespace ItAcademy.Models
{
    public class Positions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employers> Employers { get; set; }
        public List<Teachers> Teachers { get; set; }
        public bool IsDeactive { get; set; }
    }
}
