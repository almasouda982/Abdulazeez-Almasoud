namespace Bootcamp_MVC_EF.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }

        public string Category { get; set; }
        public float Price { get; set; }
    }
}
