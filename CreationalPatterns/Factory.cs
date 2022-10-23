using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternPractices.CreationalPatterns
{
    ///
    /// Factory (抽象工廠)          MealFactory                         定義抽象方法供實做
    /// ConcreteFactory (實體工廠)  SteakFactory, ChickenFactroy, ...   實作 Factory 並建立物件
    /// Product (抽象產品)          CookMeal                            定義抽象方法供實作 
    /// ConcreteProduct (實體產品)  Steak, Checken, Pork                實作產品的商業邏輯
    /// 

    /// <summary>
    /// Cook Meal 介面
    /// </summary>
    public interface CookMeal
    {
        void Cook();
        void Delivery();
    }

    public class Steak : CookMeal
    {
        public void Cook()
        {
            Console.WriteLine($"把牛排煮熟");
        }

        public void Delivery()
        {
            Console.WriteLine($"送牛排");
        }
    }


    public class Chicken : CookMeal
    {
        public void Cook()
        {
            Console.WriteLine($"把雞肉煮熟");
        }

        public void Delivery()
        {
            Console.WriteLine($"送雞肉");
        }
    }

    public class Pork : CookMeal
    {
        public void Cook()
        {
            Console.WriteLine($"把豬排煮熟");
        }

        public void Delivery()
        {
            Console.WriteLine($"送豬排");
        }
    }


    public class Restaurant
    {
        private MealFactory _factory;

        public Restaurant(MealFactory factory)
        {
            _factory = factory;
        }

        public CookMeal MealOrder()
        {
            CookMeal meal;

            meal = _factory.CreateMeal();

            meal.Cook();
            meal.Delivery();
            return meal;
        }
    }


#if false
    public class MealFactory
    {
        public CookMeal CreateMeal(string mealType)
        {
            CookMeal meal = null;


            if (nameof(Steak).Equals(mealType))
            {
                meal = new Steak();
            }
            else if (nameof(Chicken).Equals(mealType))
            {
                meal = new Chicken();
            }
            else if (nameof(Pork).Equals(mealType))
            {
                meal = new Pork();
            }

            return meal;
        }
    } 
#endif

    public interface MealFactory
    {
        public CookMeal CreateMeal();
    }


    public class SteakFactory : MealFactory
    {
        public CookMeal CreateMeal()
        {
            return new Steak();
        }
    }

    public class ChickenFactory : MealFactory
    {
        public CookMeal CreateMeal()
        {
            return new Chicken();
        }
    }

    public class PorkFactory : MealFactory
    {
        public CookMeal CreateMeal()
        {
            return new Pork();
        }
    }
}
