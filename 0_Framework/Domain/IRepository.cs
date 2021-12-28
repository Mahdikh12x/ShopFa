using System.Linq.Expressions;

namespace _0_Framework.Domain
{
    public interface IRepository<TKey, T> where T : class
    {
        void Create(T entity);
        void SaveChanges();
        T Get(TKey key);
        List<T> Get();
        bool Exists(Expression<Func<T, bool>> expression);
    }
}
