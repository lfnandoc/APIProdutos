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
