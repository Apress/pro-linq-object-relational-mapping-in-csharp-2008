using System;
using System.Collections.Generic;
using System.Text;
using BoP.Core;
using BoP.Core.Domain;

namespace BoP.Workers
{
    public class UserFactory: IUserFactory
    {
        #region IUserFactory Members

        public User Create()
        {
            //
            throw new NotImplementedException();
        }

        public User Create(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public User FindByUserName(string username)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
