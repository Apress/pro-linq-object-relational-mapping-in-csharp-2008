using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Linq.Mapping;
using BoP.Core.DataInterfaces;
using BoP.Core;
using System.Data.Entity;

namespace BoP.Data.LTS
{
    public abstract class AbstractLTSDao<T, IdT> : IDao<T, IdT> where T:class
    {

        #region IDao<T,IdT> Members

        BoPDataContext db = BoPDataContextManager.Instance.GetContext();

        public virtual T GetById(IdT id)
        {
           
           return default(T);
            
           
        }

        public virtual T Update(T newEntity, T oldEntity)
        {
            return default(T);
        }

        public virtual List<T> GetAll()
        {
            Table<T> someTable = db.GetTable(typeof(T)) as Table<T>;
            return someTable.ToList<T>();
        }

        public virtual T Save(T entity)
        {
            ITable tab = db.GetTable(entity.GetType().BaseType);
            tab.InsertOnSubmit(entity);
            db.SubmitChanges();
            return entity;
        }

        public virtual T SaveOrUpdate(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(T entity)
        {
            ITable tab = db.GetTable(entity.GetType().BaseType);
            tab.DeleteOnSubmit(entity);
            db.SubmitChanges();
            
        }

        public virtual void CommitChanges()
        {
            db.SubmitChanges();
        }

        #endregion
    }
}
