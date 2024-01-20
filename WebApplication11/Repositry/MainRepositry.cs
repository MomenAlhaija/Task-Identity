using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Identity.Areas.Data;
using Identity.Repositry.Base;

namespace Identity.Repositry
{
    public class MainRepositry<T> : IRepositry<T> where T : class
    {
        private readonly AppDbContext _dbcontext;
        public MainRepositry(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public T FindById(int? id)
        {
            return _dbcontext.Set<T>().Find(id);
        }

        public async Task<T> FindByIdAsync(int? id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);

        }

        public IEnumerable<T> GetAll()
        {
            return _dbcontext.Set<T>().ToList();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public T selectOne(Expression<Func<T, bool>> match)
        {
            return _dbcontext.Set<T>().SingleOrDefault(match);
        }

        public async Task<IEnumerable<T>> GetAllAsync(params string[] args)
        {
            IQueryable<T> query = _dbcontext.Set<T>();
            if(args.Length > 0)
            {
                foreach(string arg in args)
                    query= query.Include(arg);
            }
            return query.ToList();
        }

        public void Add(T item)
        {
           _dbcontext.Set<T>().Add(item);
           _dbcontext.SaveChanges();
        }

        public void UpdateOne(T item)
        {
            _dbcontext.Set<T>().Update(item);
            _dbcontext.SaveChanges();
        }

        public void Delete(T item)
        {
            _dbcontext.Set<T>().Remove(item);
            _dbcontext.SaveChanges();
        }

        public void AddList(IEnumerable<T> items)
        {
            _dbcontext.Set<T>().AddRange(items);
            _dbcontext.SaveChanges();
        }

        public void UpdateList(IEnumerable<T> items)
        {
            _dbcontext.Set<T>().UpdateRange(items);
            _dbcontext.SaveChanges();
        }

        public void DeleteList(IEnumerable<T> items)
        {
           _dbcontext.Set<T>().RemoveRange(items);
           _dbcontext.SaveChanges();
        }

        public async void AddAsync(T item)
        {
           await _dbcontext.Set<T>().AddAsync(item);
           _dbcontext.SaveChanges();
        }
       
        public async void AddListAsync(IEnumerable<T> items)
        {
             await  _dbcontext.Set<T>().AddRangeAsync(items);
             _dbcontext.SaveChanges();
        }

    }
}
