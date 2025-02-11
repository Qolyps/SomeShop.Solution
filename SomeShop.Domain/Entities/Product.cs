using System.Text.Json.Serialization;

namespace SomeShop.Domain.Entities
{
    public class Product
    {
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
