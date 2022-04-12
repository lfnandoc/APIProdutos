using IntercambioGenebraAPI.Infra.DB;
using System.Data;

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
    }
}
