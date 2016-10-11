using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoP.Core.Domain;
using BoP.Core.DataInterfaces;

namespace BoP.Data.LTS
{
    public class UserDao : AbstractLTSDao<User, int>, IUserDao
    {

        BoPDataContext db = BoPDataContextManager.Instance.GetContext();



        public  User GetByUserIdAndPassword(string uid, string pwd)
        {

            User u = (from p in db.GetTable<Role>().OfType<User>()
                      where p.UserId == uid &&
                      p.Password == pwd
                      select p).First();

            return u;


        }


        #region IUserDao Members

        public User GetByUserId(string id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
