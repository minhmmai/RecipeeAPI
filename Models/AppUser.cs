using Microsoft.AspNetCore.Identity;
using RecipeeAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeeAPI.Models
{
    public class AppUser : BaseEntity
    {
        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(50)]
        [Required]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        [StringLength(255)]
        [Required]
        public string Email { get; set; }
        public string PictureUrl { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
