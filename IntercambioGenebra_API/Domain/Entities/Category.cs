namespace IntercambioGenebraAPI.Domain.Entities
{
    public class Category : Entity
    {
        public string Name { get; protected set; }

        public Category(string name)
        {
            Name = name;
        }

        public Category()
        {
        }

        public void ChangeName(string name) => Name = name;
    }
}