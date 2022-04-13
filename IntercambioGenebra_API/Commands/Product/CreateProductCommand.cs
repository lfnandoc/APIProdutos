namespace IntercambioGenebraAPI.Commands.Product
{
    public class UpdateProductCommand
    {
        public string Name { get; set; }
        public decimal Price { get; set; }        
        public Guid CategoryId { get; set; }
    }
}
