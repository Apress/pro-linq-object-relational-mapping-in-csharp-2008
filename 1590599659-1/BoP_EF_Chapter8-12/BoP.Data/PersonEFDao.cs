using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoP.Core.Domain;
using BoP.Core.DataInterfaces;
using System.Data;
using System.Data.Objects;

namespace BoP.Data.EF
{
    public class PersonEFDao : AbstractEFDao<Person, int>, IPersonDao
    {

        //BoPObjectContext db = BoPObjectContextManager.Instance.GetContext();
        BoPObjectContext db = new BoPObjectContext();


        public override Person GetById(int id)
        {
            IEnumerable<KeyValuePair<string, object>> entityKeyValues =
                new KeyValuePair<string, object>[] { 
                    new KeyValuePair<string, object>("StakeHolderId", id)};


            EntityKey ek = new EntityKey("StakeHolder", entityKeyValues);
            object o = db.GetObjectByKey(ek);
            return (Person)o;



        }

        public Person GetByTaxId(string taxid)
        {

            Person sh = (from p in db.StakeHolder.OfType<Person>()
                         where p.TaxId == taxid
                         select p).First();

            return sh;

        }

        //public override Person Update(Person newEntity, Person oldEntity)
        //{

        //    try
        //    {
        //        ObjectStateEntry entry;

        //        // Get the entity key of the updated object.
        //        EntityKey key = db.GetEntityKey("StakeHolder", oldEntity);

        //        // Attach the original item to the object context,
        //        // if it is not already attached or if the entry is 
        //        // for the relationship and not the object itself.
        //        if (!db.ObjectStateManager
        //            .TryGetObjectStateEntry(key, out entry)
        //            || (entry.Entity == null))
        //        {
        //            db.Attach(oldEntity);
        //        }

        //        // Call the ApplyPropertyChanges method to apply changes
        //        // from the updated item to the original version.
        //        db.ApplyPropertyChanges(
        //            key.EntitySetName, newEntity);
        //        ObjectStateEntry en = db.ObjectStateManager.GetObjectStateEntry(key);
        //        en.SetModified();
        //        db.Refresh(RefreshMode.ClientWins, oldEntity);
        //        db.SaveChanges();
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    return newEntity;

        //}

        public List<Person> GetByExample(Person p)
        {
            List<Person> lp = (from s in db.StakeHolder.OfType<Person>()
                               where s.TaxId == p.TaxId ||
                               s.StakeHolderId == p.StakeHolderId ||
                               s.LastName == p.LastName ||
                               s.Gender == p.Gender ||
                               s.Email == p.Email ||
                               s.DOB == p.DOB ||
                               s.FirstName == p.FirstName
                               select s).ToList();
            return lp;


        }

    }
}
