using Manager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Infrastructure.Interfaces
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<T> Create(T obj);
        
        Task<T> Update(T obj);

        Task Remove(Guid id);

        Task<T> Get(Guid id);

        Task<List<T>> GetAll();
    }
}
