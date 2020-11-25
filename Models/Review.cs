using RecipeeAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeeAPI.Models
{
    [Table("Review")]
    public class Review : BaseEntity
    {
        [Range(1,5)]
        public byte Rating { get; set; }
        [StringLength(1000)]
        public string Feedback { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public AppUser User { get; set; }
    }
}
