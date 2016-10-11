using System.Collections.Generic;

namespace BoP.Core.DataInterfaces
{
    /// <summary>
    /// This interface was adapted from Billy Mccafferty's NHibernate Framework
    /// <see cref="http://devlicio.us/blogs/billy_mccafferty"/>
    /// 
    /// The purpose of this interface is to provide a
    /// general purpose contract for CRUD operations
    /// executed in the ORM layer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="IdT"></typeparam>
    public interface IDao<T, IdT>
    {
        T GetById(IdT id);
        List<T> GetAll();
        T Save(T entity);
        T Update(T entity, T originalEntity);
        void Delete(T entity);
        void CommitChanges(object entity);
    }
}
