namespace API.Contracts
{
    public interface IGeneralRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity? GetByGuid(Guid guid);
        TEntity? Create(TEntity obj);
        bool Update(TEntity obj);
        bool Delete(TEntity obj);
        void Clear();
    }
}
