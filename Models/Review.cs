using RecipeeAPI.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeeAPI.Models
{
    [Table("Review")]
    public class Review : BaseEntity
    {
        [Range(1,5)]
        public byte Rating { get; set; }
        [StringLength(500)]
        public string Feedback { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
