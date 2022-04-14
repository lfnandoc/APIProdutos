namespace IntercambioGenebraAPI.Domain.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }

        public decimal Price { get; set; } = 0;

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}