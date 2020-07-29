using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeeAPI.DTOs.Ingredient
{
    public class GetIngredientDTO
    {
        public decimal? Quantity { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
    }
}
