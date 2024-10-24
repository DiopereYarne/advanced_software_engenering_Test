using RestaurantOrderSystem.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RestaurantOrderSystem.Repositories
{
    public interface IOrderRepository
    {
        void SaveOrder(Order order);
        List<Order> GetOrders();
    }

    public interface IMenuRepository
    {
        List<Food> GetFoodMenu();
        List<Drink> GetDrinkMenu();
    }

    public class CsvOrderRepository : IOrderRepository
    {
        private const string OrdersFilePath = "Orders.csv";

        public void SaveOrder(Order order)
        {
            var orderLine = $"{order.Id};{order.CustomerName};{order.Street};{order.ZipCode};{order.City};{order.Phone};{order.TotalPrice};";
            File.AppendAllText(OrdersFilePath, orderLine + "\n");
        }

        public List<Order> GetOrders()
        {
            if (!File.Exists(OrdersFilePath)) return new List<Order>();

            var lines = File.ReadAllLines(OrdersFilePath).Skip(1); // Skip header
            return lines.Select(line =>
            {
                var data = line.Split(';');
                return new Order
                {
                    Id = int.Parse(data[0]),
                    CustomerName = data[1],
                    Street = data[2],
                    ZipCode = data[3],
                    City = data[4],
                    Phone = data[5],
                    TotalPrice = data[6] == "" ? 0 : decimal.Parse(data[6])
                };
            }).ToList();
        }
    }

    public class CsvMenuRepository : IMenuRepository
    {
        private const string FoodFilePath = "./csv/Food.csv";
        private const string DrinkFilePath = "./csv/Drinks.csv";

        public List<Food> GetFoodMenu()
        {
            return File.ReadAllLines(FoodFilePath).Skip(1).Select(line =>
            {
                var data = line.Split(';');
                return new Food
                {
                    Id = int.Parse(data[0]),
                    Name = data[1],
                    Price = decimal.Parse(data[2]),
                    Weight = double.Parse(data[3]),
                    IsSpicy = bool.Parse(data[4]),
                    IsVegan = bool.Parse(data[5])
                };
            }).ToList();
        }

        public List<Drink> GetDrinkMenu()
        {
            return File.ReadAllLines(DrinkFilePath).Skip(1).Select(line =>
            {
                var data = line.Split(';');
                return new Drink
                {
                    Id = int.Parse(data[0]),
                    Name = data[1],
                    Price = decimal.Parse(data[2]),
                    Volume = int.Parse(data[3]),
                    ContainsAlcohol = bool.Parse(data[4])
                };
            }).ToList();
        }
    }
}
