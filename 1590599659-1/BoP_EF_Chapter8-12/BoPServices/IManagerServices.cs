using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BoP.Core;
using BoP.Data;
using BoP.Core.Domain;
using BoP.Util;
using BoP.Core.DataInterfaces;
using BoP.Workers;
using System.Xml.Serialization;

namespace BoPServices
{
    [ServiceContract]
    public interface IManagerServices
    {
        [OperationContract]
        [ServiceKnownType(typeof(User))]
        Person GetPersonByTaxId(string taxId);

      
        [OperationContract]
        Person UpdatePerson(Person newP, Person oldP);
    }
}
