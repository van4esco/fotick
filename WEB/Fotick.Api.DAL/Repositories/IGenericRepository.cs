using Fotick.Api.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotick.Api.DAL.Repositories
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> FirstOrDefault();
        Task<T> FindById(Guid id);
        Task<int> Add(T entity);
        Task<int> Delete(Guid id);
        Task<int> Update(T entity);
    }
}
