using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using BoP.Core.Domain;
using BoP.Core.DataInterfaces;

namespace BoP.Data.LTS
{
    public class StakeHolderLTSDao: AbstractLTSDao<StakeHolder,string>,IStakeHolderDao
    {
        BoPDataContext db = BoPDataContextManager.Instance.GetContext();

        
        public override List<StakeHolder> GetAll()
        {

            return base.GetAll();
        }

        public override StakeHolder GetById(string id)
        {

            StakeHolder sh = (from p in db.StakeHolders
                             where p.StakeHolderId == id
                              select p).First();
            
            return sh;


        }

    
    }
}
