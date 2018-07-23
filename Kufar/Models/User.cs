﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Kufar.Models
{
    public class User : IdentityUser
    {
        [Required]
        [Display(Name = "Year")]
        public int Year { get; set; }
    }
}
