using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Infra;
using Microsoft.EntityFrameworkCore;

namespace IntercambioGenebraAPI.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
        
        void Save();

    }
}
