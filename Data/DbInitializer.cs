using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipeeAPI.Models;

namespace RecipeeAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(RecipeeContext context)
        {
            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User { FirstName = "Carson",   LastName = "Alexander",
                    Email = "kmisk0@dedecms.com"},
                new User { FirstName = "Meredith", LastName = "Alonso",
                    Email = "rlittledyke1@google.com.br" },
                new User { FirstName = "Arturo",   LastName = "Anand",
                    Email = "wdare2@home.pl" },
                new User { FirstName = "Gytis",    LastName = "Barzdukas",
                    Email = "wbarcroft3@etsy.com" }
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            var recipes = new Recipe[]
            {
                new Recipe {Name = "Quick & easy omelette", Description = "Mastering the art of cooking an omelette means you'll never be stuck for an easy dinner again. This step-by-step, three-egg omelette recipe can be easily adapted with the filling of your choice.", Serves = 1, UserId = 1 },
                new Recipe {Name = "2-ingredient vegan condensed milk", Description = "This simple homemade condensed milk recipe needs only 2 ingredients. Use it in all your favourite slice and fudge recipes.", Serves = 1, UserId = 2 },
                new Recipe {Name = "Salted caramel cookie cups", Description = "Wow your guests with our easiest-ever dessert cups.", Serves = 24, UserId = 3 },
            };

            context.Recipes.AddRange(recipes);
            context.SaveChanges();

            var ingredients = new Ingredient[]
            {
                new Ingredient { Quantity = 3, Name = "eggs", RecipeId = 1 },
                new Ingredient { Quantity = 20, Unit = "g", Name = "butter", RecipeId = 1  },
                new Ingredient { Quantity = 800, Unit = "ml", Name = "cans coconut milk", RecipeId = 2  },
                new Ingredient { Quantity = 1, Unit = "cup", Name = "caster sugar", RecipeId = 2  },
                new Ingredient { Quantity = 24, Name = "Arnott’s Choc Ripple biscuits", RecipeId = 3  },
                new Ingredient { Quantity = 380, Unit = "g", Name = "can Nestlé Top ‘n’ Fill Caramel", RecipeId = 3  },
                new Ingredient { Name = "Sea salt flakes, to serve", RecipeId = 3  }
            };

            context.Ingredients.AddRange(ingredients);
            context.SaveChanges();

            var methods = new Method[]
            {
                new Method { Index = 1, Detail = "Lightly beat eggs with a whisk or fork until just combined. Season with salt and pepper.", RecipeId = 1 },
                new Method { Index = 2, Detail = "Heat butter in a 20cm non-stick frypan over medium-high heat. When it starts to foam, add egg and shake pan to distribute, gently stirring with a spatula or wooden spoon. As egg begins to cook at the edges, use the spatula to draw cooked egg in towards the centre (without breaking up), allowing the uncooked egg to run out towards the edge.", RecipeId = 1 },
                new Method { Index = 3, Detail = "After 30 seconds, the egg should be just set but still soft. (You want a soft, creamy centre without too much liquid - it will keep cooking once it's removed from the heat.) Add fillings down the centre of the pan, then use the fork or a spatula to fold one side of the omelette over the filling. Hold a warmed plate next to pan, then tilt pan at an angle and slide omelette onto the plate fold-side down. Sprinkle with grated cheese or parmesan if using and serve immediately.", RecipeId = 1 },
                new Method { Index = 1, Detail = "Combine coconut milk and caster sugar in a large, heavy-based saucepan over medium heat. Cook, stirring often, for 8 minutes until sugar has dissolved and mixture comes to a simmer.", RecipeId = 2 },
                new Method { Index = 2, Detail = "Reduce heat to medium-low. Simmer, stirring often, for 40 minutes or until mixture thickens and coats the back of a wooden spoon (mixture should resemble thick custard). Remove from heat. Allow to cool. Transfer to an airtight container.", RecipeId = 2 },
                new Method { Index = 1, Detail = "Preheat oven to 180C/160C fan forced. Place a biscuit over 6 holes of a 12-hole patty pan. Bake for 2 minutes or until softened slightly. Use a teaspoon to carefully press biscuits into holes to form cups. Cool, then transfer to a wire rack. Repeat in 3 more batches.", RecipeId = 3 },
                new Method { Index = 2, Detail = "Place the caramel in a bowl and whisk until smooth. Divide among the cookie cups. Place in the fridge for 30 minutes or until firm.", RecipeId = 3 },
                new Method { Index = 3, Detail = "Sprinkle the cookie cups with a little salt just before serving.", RecipeId = 3 }
            };

            context.Methods.AddRange(methods);
            context.SaveChanges();

            var reviews = new Review[]
            {
                new Review { Rating = 3, Feedback = "This recipe is ok, can be better", RecipeId = 1},
                new Review { Rating = 5, Feedback = "Truly quick and easy, sometimes I need just this", RecipeId = 1},
                new Review { Rating = 5, Feedback = "Nice recipe", RecipeId = 1},
                new Review { Rating = 2, Feedback = "Couldn't get the same looks as the cakes shown in the pics :(", RecipeId = 2},
                new Review { Rating = 4, Feedback = "", RecipeId = 2},
                new Review { Rating = 4, Feedback = "", RecipeId = 3},
                new Review { Rating = 5, Feedback = "Super Delicous and easy to make", RecipeId = 3}
            };

            context.Reviews.AddRange(reviews);
            context.SaveChanges();
        }
    }
}