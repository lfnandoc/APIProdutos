namespace IntercambioGenebraAPI.Domain.Entities
{
    public class Product : Entity
    {
        public string Name { get; protected set; }

        public decimal Price { get; protected set; } = 0;

        public Guid CategoryId { get; protected set; }

        public virtual Category Category { get; protected set; }

        public Product(string name, Category category, decimal? price = null)
        {
            Name = name;
            CategoryId = category.Id;
            Category = category;
            Price = price ?? 0;
        }

        public Product()
        {
        }

        public void ChangeName(string name) => Name = name;

        public void ChangePrice(decimal? price) => Price = price ?? 0;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }
    }
}