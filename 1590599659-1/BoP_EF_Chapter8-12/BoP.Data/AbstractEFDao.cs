using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoP.Core.DataInterfaces;
using BoP.Core;
using BoP.Core.Domain;
using System.Data.Objects;
using System.Data;
using BoPObjectContext = BoP.Data.BoPObjectContext;

namespace BoP.Data.EF
{
    public class AbstractEFDao<T, IdT> : IDao<T, IdT> where T:class
    {
        //EFSamplesEntities db = new EFSamplesEntities();
        BoPObjectContext db = new BoPObjectContext();

        public virtual T GetById(IdT id)
        {
            return default(T);
            
        }

        public virtual List<T> GetAll()
        {
            ObjectQuery<T> oq = db.CreateQuery<T>("[" + typeof(T).Name + "]");
            
            return oq.ToList<T>();
        }

        public virtual T Save(T entity)
        {
            db.AddObject(typeof(T).Name, entity as object);
            db.SaveChanges();
            return entity;
        }

        public virtual T Update(T newEntity, T oldEntity)
        {

            EntityKey key ;
            if (oldEntity == null)
            {
                
                db.AttachTo(typeof(T).BaseType.Name, newEntity as object);
                key = db.CreateEntityKey(typeof(T).BaseType.Name, newEntity);
                ObjectStateEntry en = db.ObjectStateManager.GetObjectStateEntry(key);
                en.SetModified();
                db.Refresh(RefreshMode.ClientWins, newEntity as object);
            
            
            }
            else 
            {
                key = db.CreateEntityKey(typeof(T).BaseType.Name, oldEntity);
                if (key == null)
                {
                    db.AttachTo(typeof(T).BaseType.Name, newEntity as object);
                }
                else
                {
                    db.Attach(oldEntity as System.Data.Objects.DataClasses.IEntityWithKey);
                    db.ApplyPropertyChanges(key.EntitySetName, newEntity as object);
                }

                ObjectStateEntry en = db.ObjectStateManager.GetObjectStateEntry(key);
                en.SetModified();
                db.Refresh(RefreshMode.ClientWins, oldEntity as object);
                

            }

            this.CommitChanges(newEntity as object);
            return newEntity;

        }

        static void SetEntryModified(ObjectStateEntry entry)
        {
            // One way of doing it, almost certainly others...
            for (int i = 0; i < entry.CurrentValues.FieldCount; i++)
            {
                bool isKey = false;

                string name = entry.CurrentValues.GetName(i);

                foreach (EntityKeyMember keyPair in entry.EntityKey.EntityKeyValues)
                {
                    if (string.Compare(name, keyPair.Key, true) == 0)
                    {
                        isKey = true;
                        break;
                    }
                }
                if (!isKey)
                {
                    entry.SetModifiedProperty(name);
                }

     
            }
        }



        public virtual void Delete(T entity)
        {
            db.DeleteObject(entity as object);
            db.SaveChanges();

        }

        public virtual void CommitChanges(object entity)
        {
            try
            {
                // Try to save changes, which may cause a conflict.
                int num = db.SaveChanges();

                // Log Message
                Console.WriteLine("No conflicts. " +
                    num.ToString() + " updates saved.");
            }
            catch (OptimisticConcurrencyException)
            {
                // Resolve the concurrently conflict by refreshing the 
                // object context before re-saving changes. 
                db.Refresh(RefreshMode.ClientWins, entity);

                // Save changes.
                db.SaveChanges();

                // Log Message
                Console.WriteLine("OptimisticConcurrencyException "
                + "handled and changes saved");
            }
            catch (Exception e)
            {
                throw (e);
            }


            //db.SaveChanges();
        }
    }

    }

