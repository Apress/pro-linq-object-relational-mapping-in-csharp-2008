using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoP.Core.Domain;
using BoP.Core.DataInterfaces;

namespace BoP.Data.LTS
{
    public class PersonDao: AbstractLTSDao<Person,int>,IPersonDao
    {

        BoPDataContext db = BoPDataContextManager.Instance.GetContext();


        public override List<Person> GetAll()
        {

            return base.GetAll();
        }

        public override Person GetById(int id)
        {

            Person sh = (from p in db.GetTable<StakeHolder>().OfType<Person>()
                              where p.StakeHolderId == id
                              select p).First();

            return sh;


        }

        public Person GetByTaxId(string taxid)
        {

            Person sh = (from p in db.GetTable<StakeHolder>().OfType<Person>()
                         where p.TaxId == taxid
                         select p).First();

            return sh;
        
        }

        public List<Person> GetByExample(Person p)
        {
            List<Person> lp = (from s in db.GetTable<StakeHolder>().OfType<Person>()
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
