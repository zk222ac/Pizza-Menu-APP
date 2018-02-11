using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App62.Model
{
    public class PizzaCatalog
    {
        public static List<Pizza> GetPizzas()
        {
            var pizzas = new List<Pizza>();

            pizzas.Add(new Pizza("Chilly", "Extra Large", 70));
            pizzas.Add(new Pizza("Chilly", "Large", 60));
            pizzas.Add(new Pizza("Chilly", "Medium", 50));
            //pizzas.Add(new Pizza("Chilly", "Small", 40));
            pizzas.Add(new Pizza("Salad", "Extra Large", 50));
            pizzas.Add(new Pizza("Salad", "Large", 40));
            //pizzas.Add(new Pizza("Salad", "Medium", 30));
            pizzas.Add(new Pizza("Salad", "Small", 20));
            pizzas.Add(new Pizza("Chicken", "Extra Large", 75));
            //pizzas.Add(new Pizza("Chicken", "Large", 65));
            pizzas.Add(new Pizza("Chicken", "Medium", 55));
            pizzas.Add(new Pizza("Chicken", "Small", 40));
            //pizzas.Add(new Pizza("Kebab", "Extra Large", 80));
            pizzas.Add(new Pizza("Kebab", "Large", 60));
            pizzas.Add(new Pizza("Kebab", "Medium", 40));
            pizzas.Add(new Pizza("Kebab", "Small", 20));

            return pizzas;
        }
    }
}
