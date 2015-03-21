using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoDragons2.Database.Interface
{
    public interface IDatabase<T> where T : class, new()
    {
        int Delete(Expression<Func<T, bool>> expression);
        bool Delete(T item);
        void DeleteAll();
        T Single(Expression<Func<T, bool>> expression);
        System.Linq.IQueryable<T> Query { get; set; }
        System.Linq.IQueryable<T> All(int page, int pageSize);
        bool Add(T item);
        int Add(IEnumerable<T> items);
    }
}
