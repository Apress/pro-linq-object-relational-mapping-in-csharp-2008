using System;
using System.Collections.Generic;
using System.Text;
using BoP.Core.Domain;

namespace BoP.Core.DataInterfaces
{
    public interface IUserDao : IDao<User, int>
    {
        User GetByUserId(string id);
        User GetByUserIdAndPassword(string uid, string pwd);
    }
}
