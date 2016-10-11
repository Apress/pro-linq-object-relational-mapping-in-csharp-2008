using System;
using System.Collections.Generic;
using System.Text;

namespace BoP.Core.Domain
{
    public interface IPersonManager
    {

        Person GetPersonByTaxId(string taxId);
        Person UpdatePerson(Person newP,Person oldP);
    }
}
