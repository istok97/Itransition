﻿using System.ComponentModel.DataAnnotations;
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
