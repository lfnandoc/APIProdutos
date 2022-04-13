﻿namespace IntercambioGenebraAPI.Commands.Product
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public decimal Price { get; set; }        
        public Guid CategoryId { get; set; }
    }
}
