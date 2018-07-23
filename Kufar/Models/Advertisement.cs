﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Kufar.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="incorrect information")]
        
        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}
