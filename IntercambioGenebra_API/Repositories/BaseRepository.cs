using IntercambioGenebraAPI.Infra;

namespace IntercambioGenebraAPI.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        private readonly AppDbContext _context;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Insert(TEntity entity)
        {
            _context.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
