using System.Data;

namespace IntercambioGenebraAPI.Entities
{
    public interface IEntity
    {
        public abstract int Id { get; set; }

        public abstract void MapEntity(DataRow dataRow);
    }
}
