﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecipeeAPI.Models.Base;

namespace RecipeeAPI.Models
{
    [Table("Recipe")]
    public class Recipe : BaseEntity
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [StringLength(500)]
        [Required]
        public string Description { get; set; }
        public int Serves { get; set; }
        public int UserId { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Method> Methods { get; set; }
        public List<Review> Reviews { get; set; }
        public User Creator { get; set; }
    }
}
