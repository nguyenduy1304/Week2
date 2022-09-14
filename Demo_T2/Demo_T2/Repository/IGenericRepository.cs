namespace Demo_T2.Repository
{
    public interface IGenericRepository<T> : IDisposable
    {
        IEnumerable<T> GetAll();
        T Get(String id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
