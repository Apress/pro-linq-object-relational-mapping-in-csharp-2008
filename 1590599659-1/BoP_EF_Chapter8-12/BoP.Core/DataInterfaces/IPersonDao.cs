using System;
using System.Collections.Generic;
using System.Text;
using BoP.Core.Domain;

namespace BoP.Core.DataInterfaces
{
    public interface IPersonDao: IDao<Person, int>
    {
        Person GetByTaxId(string taxid);
        List<Person> GetByExample(Person p);
    }
}
