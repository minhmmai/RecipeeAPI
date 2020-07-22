﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecipeeAPI.Models.Base;
using System.ComponentModel;

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
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Method> Methods { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public User Creator { get; set; }
    }
}
