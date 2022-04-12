using IntercambioGenebraAPI.Infra.DB;
using System.Data;
using System.Text;

namespace IntercambioGenebraAPI.Entities
{
    public class Category : Entity
    {
        [Column("name")]
        public string? Name { get; set; }             

        public Category() : base()
        {
            table = "categories";
        }

        public override void MapEntity(DataRow dataRow)
        {
            base.MapEntity(dataRow);
            Name = dataRow["name"].ToString();
        }

        public bool HasAssociatedProducts()
        {
            var query = new StringBuilder()
            .Append($"SELECT COUNT(*) FROM products")
            .Append($" WHERE categoryId = {Id}")
            .ToString();

            var result = Convert.ToInt32(new DBConnection().SelectScalar(query));

            return result > 0;
        }
    }
}
