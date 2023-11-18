﻿using cat_a_logB.Pages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cat_a_logB.Data
{
    public class Member
    {
        [Key, Column(Order = 1)]
        public int UserId { get; set; }

        [Key, Column(Order = 2)]
        public int TeamId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("TeamId")]
        public ProjectTeam Team { get; set; }

        public string Name { get; set; }
        public string Position { get; set; }
        public float Efficiency { get; set; }
    }
}