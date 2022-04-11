using IntercambioGenebraAPI.Infra.DB;
using System.Data;

namespace IntercambioGenebraAPI.Entities
{
    public class Product : Entity
    {
        [Column("name")]
        public string? Name { get; set; }             

        [Column("price")]
        public decimal? Price { get; set; }

        public Product() : base()
        {
            table = "products";
        }

        public override void MapEntity(DataRow dataRow)
        {
            base.MapEntity(dataRow);
            Name = dataRow["name"].ToString();
            Price = Convert.ToDecimal(dataRow["price"]);
        }
    }
}
