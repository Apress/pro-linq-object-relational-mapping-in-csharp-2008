using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoP.Core.Domain;
using BoP.Core.DataInterfaces;

namespace BoP.Data.EF
{
    public class UserDao : AbstractEFDao<User, int>, IUserDao
    {

        BoPObjectContext db = BoPObjectContextManager.Instance.GetContext();



        public User GetByUserIdAndPassword(string uid, string pwd)
        {

            User u = (from p in db.User
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

