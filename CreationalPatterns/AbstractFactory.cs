using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternPractices.CreationalPatterns
{
    public interface Ingredient
    {
        public void GetSauce();
    }


    public class TWIngredient : Ingredient
    {
        public void GetSauce()
        {
            Console.WriteLine($"淋上台式醬料");
        }
    }

    public class ITIngredient : Ingredient
    {
        public void GetSauce()
        {
            Console.WriteLine($"淋上義式醬料");
        }
    }


    public abstract class AbstractRestaurant
    {
        public CookMeal MealOrder(string type)
        {
            CookMeal meal;
            Ingredient ingredient;

            meal = CreateMeal(type);
            ingredient = GetIngredient();

            meal.Cook();
            ingredient.GetSauce();
            return meal;
        }

        public abstract CookMeal CreateMeal(string type);

        public abstract Ingredient GetIngredient();
    }

    public class TWRestaurant : AbstractRestaurant
    {
        public override CookMeal CreateMeal(string type)
        {
            CookMeal meal = null;
            if (nameof(Steak).Equals(type))
            {
                meal = new Steak();
            }
            else if (nameof(Pork).Equals(type))
            {
                meal = new Pork();
            }
            return meal;
        }

        public override Ingredient GetIngredient()
        {
            Ingredient ingredient = new TWIngredient();
            return ingredient;
        }
    }

    public class ITRestaurant : AbstractRestaurant
    {
        public override CookMeal CreateMeal(string type)
        {
            CookMeal meal = null;
            if (nameof(Steak).Equals(type))
            {
                meal = new Steak();
            }
            else if (nameof(Pork).Equals(type))
            {
                meal = new Pork();
            }
            return meal;
        }

        public override Ingredient GetIngredient()
        {
            Ingredient ingredient = new ITIngredient();
            return ingredient;
        }
    }

}
