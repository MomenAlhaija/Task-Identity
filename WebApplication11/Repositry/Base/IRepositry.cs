using System.Linq.Expressions;

namespace Identity.Repositry.Base
{
    public interface IRepositry<T> where T : class
    {
        T FindById(int? id);
        T selectOne(Expression<Func<T, bool>> match);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(params string[] args);
        Task<T> FindByIdAsync(int? id);

        void Add(T item);
        void UpdateOne(T item);
        void Delete(T item);
        void AddList(IEnumerable<T> items);
        void UpdateList(IEnumerable<T> items);
        void DeleteList(IEnumerable<T> items);
        void AddAsync(T item);
        void AddListAsync(IEnumerable<T> items);
     

    }
}
