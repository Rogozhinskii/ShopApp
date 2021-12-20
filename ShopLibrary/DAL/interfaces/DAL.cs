using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.DAO.interfaces
{
    public interface DAL<T>
    {
        public T GetById(int id);
        public List<T> GetAll();
        public bool Update(T entity);
        public bool Delete(int id);
        public bool Insert(T entity);
        
    }
}
