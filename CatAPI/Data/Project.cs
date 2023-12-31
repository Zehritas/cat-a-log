﻿using System.ComponentModel.DataAnnotations;

namespace CatAPI.Data
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}