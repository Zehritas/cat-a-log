﻿using System.ComponentModel.DataAnnotations;

namespace cat_a_logB.Data
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}