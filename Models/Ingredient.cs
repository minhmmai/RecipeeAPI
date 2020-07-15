using RecipeeAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeeAPI.Models
{
    [Table("Ingredient")]
    public class Ingredient : BaseEntity
    {
        [Column(TypeName = ("decimal(5,2)"))]
        [Required]
        public decimal Quantity { get; set; }
        [StringLength(20)]
        public string Unit { get; set; }
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
