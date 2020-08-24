using Microsoft.AspNetCore.Identity;
using RecipeeAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeeAPI.Models
{
    [Table("User")]
    public class User
    {
        [Required]
        public string Id { get; set; }
        public List<Recipe> Recipes { get; set; }
        [Timestamp]
        public byte[] TimeStamp { get; set; }
    }
}
