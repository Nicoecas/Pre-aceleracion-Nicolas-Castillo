using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreAceleracion.Data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        //Delete and Get generic
        Task<T> Delete(int id);
        Task<List<T>> GetAll();
        Task<T> GetById(int id);

    }
}
