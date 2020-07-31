using RecipeeAPI.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeeAPI.Models
{
    [Table("Method")]
    public class Method : BaseEntity
    {
        [Required]
        public short Index { get; set; }
        [StringLength(500)]
        [Required]
        public string Detail { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
