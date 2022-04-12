using IntercambioGenebraAPI.Infra.DB;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace IntercambioGenebraAPI.Entities
{
    public class Product : Entity
    {
        [Column("name")]
        public string? Name { get; set; }             

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("categoryId")]
        public int? CategoryId { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public string? CategoryName
        {
            get
            {
                if (CategoryId != null)
                    return (new Category().GetById((int)CategoryId) as Category)?.Name ?? string.Empty;

                return string.Empty;
            }
        }

        public Product() : base()
        {
            table = "products";
        }

        public override void MapEntity(DataRow dataRow)
        {
            base.MapEntity(dataRow);
            Name = dataRow["name"].ToString();
            Price = Convert.ToDecimal(dataRow["price"]);
            CategoryId = Convert.ToInt32(dataRow["categoryId"]);
        }
    }
}
