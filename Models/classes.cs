namespace RestaurantOrderSystem.Models
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Volume { get; set; }
        public bool ContainsAlcohol { get; set; }
    }

    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Weight { get; set; }
        public bool IsSpicy { get; set; }
        public bool IsVegan { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
