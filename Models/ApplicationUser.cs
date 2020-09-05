using Microsoft.AspNetCore.Identity;
using RecipeeAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeeAPI.Models
{
    [Table("User")]
    public class ApplicationUser : IdentityUser
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
        public string PictureUrl { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}
