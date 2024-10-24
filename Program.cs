using RestaurantOrderSystem.Repositories;
using RestaurantOrderSystem.Services;
using System;

namespace RestaurantOrderSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var orderRepository = new CsvOrderRepository();
            var menuRepository = new CsvMenuRepository();
            var orderService = new OrderService(orderRepository, menuRepository);

            while (true)
            {
                Console.WriteLine("1. Create Order");
                Console.WriteLine("2. Show Orders");
                Console.WriteLine("9. Exit");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        orderService.CreateOrder();
                        break;
                    case "2":
                        orderService.ShowOrders();
                        break;
                    case "9":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
