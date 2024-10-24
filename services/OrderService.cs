using RestaurantOrderSystem.Models;
using RestaurantOrderSystem.Repositories;
using System;
using System.Collections.Generic;

namespace RestaurantOrderSystem.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMenuRepository _menuRepository;

        public OrderService(IOrderRepository orderRepository, IMenuRepository menuRepository)
        {
            _orderRepository = orderRepository;
            _menuRepository = menuRepository;
        }

        public void CreateOrder()
        {
            // Vraag klantgegevens
            Console.WriteLine("Enter customer name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter street:");
            var street = Console.ReadLine();
            Console.WriteLine("Enter postal code (max 4 digits):");
            var zipCode = Console.ReadLine();
            if (zipCode.Length > 4)
            {
                Console.WriteLine("Error: Postal code should not be longer than 4 digits.");
                return;
            }
            Console.WriteLine("Enter city:");
            var city = Console.ReadLine();
            Console.WriteLine("Enter phone number:");
            var phone = Console.ReadLine();

            // Bestellen van maaltijden
            var foodMenu = _menuRepository.GetFoodMenu();
            var drinksMenu = _menuRepository.GetDrinkMenu();

            decimal totalPrice = 0;

            Console.WriteLine("Select food (type the number):");
            foreach (var food in foodMenu)
            {
                Console.WriteLine($"{food.Id}. {food.Name} - €{food.Price}");
            }

            var foodOrders = new List<(Food, int)>();
            while (true)
            {
                Console.WriteLine("Choose a food item or type 'done' to finish:");
                var input = Console.ReadLine();
                if (input == "done") break;

                var foodChoice = int.Parse(input);
                var selectedFood = foodMenu.Find(f => f.Id == foodChoice);

                Console.WriteLine($"How many {selectedFood.Name} do you want?");
                var quantity = int.Parse(Console.ReadLine());

                foodOrders.Add((selectedFood, quantity));
                totalPrice += selectedFood.Price * quantity;
            }

            // Bestellen van drankjes
            Console.WriteLine("Select drinks (type the number):");
            foreach (var drink in drinksMenu)
            {
                Console.WriteLine($"{drink.Id}. {drink.Name} - €{drink.Price}");
            }

            var drinkOrders = new List<(Drink, int)>();
            while (true)
            {
                Console.WriteLine("Choose a drink item or type 'done' to finish:");
                var input = Console.ReadLine();
                if (input == "done") break;

                var drinkChoice = int.Parse(input);
                var selectedDrink = drinksMenu.Find(d => d.Id == drinkChoice);

                Console.WriteLine($"How many {selectedDrink.Name} do you want?");
                var quantity = int.Parse(Console.ReadLine());

                drinkOrders.Add((selectedDrink, quantity));
                totalPrice += selectedDrink.Price * quantity;
            }

            // Order opslaan
            var order = new Order
            {
                Id = new Random().Next(1, 1000), // Random ID voor voorbeeld
                CustomerName = name,
                Street = street,
                ZipCode = zipCode,
                City = city,
                Phone = phone,
                TotalPrice = totalPrice
            };

            _orderRepository.SaveOrder(order);

            // Order samenvatting tonen
            Console.WriteLine($"{name} - {street} - {zipCode} - {city} - {phone}");
            Console.WriteLine($"Total price: €{totalPrice}");
        }

        public void ShowOrders()
        {
            var orders = _orderRepository.GetOrders();
            if (orders.Count == 0)
            {
                Console.WriteLine("No orders found.");
                return;
            }
            foreach (var order in orders)
            {
                Console.WriteLine($"Order: {order.CustomerName} - {order.Street} - {order.ZipCode} - {order.City} - {order.Phone} - €{order.TotalPrice}");
            }
        }
    }
}
