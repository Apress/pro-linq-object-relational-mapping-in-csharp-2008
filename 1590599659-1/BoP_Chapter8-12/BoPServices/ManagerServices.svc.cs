﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BoP.Core;
using BoP.Data;
using BoP.Core.Domain;
using BoP.Util;
using BoP.Data.LTS;
using BoP.Core.DataInterfaces;
using BoP.Workers;
using System.Xml.Serialization;

namespace BoPServices
{
    // NOTE: If you change the class name "ManagerServices" here, you must also update the reference to "ManagerServices" in Web.config.
    public class ManagerServices : IManagerServices
    {
        IServiceFactory serviceFactory;

        [ServiceKnownType(typeof(User))]
        public Person GetPersonByTaxId(string taxId)
        {

            serviceFactory = new ClassServiceFactory();
            IPersonManager um = (IPersonManager)serviceFactory.FindByServiceName("BoP/Core/Domain/IPersonManager");
            Person p = um.GetPersonByTaxId(taxId);
            return p;
        }


        public Person UpdatePerson(Person newPerson, Person originalPerson)
        {
            
            serviceFactory = new ClassServiceFactory();
            IPersonManager um = (IPersonManager)serviceFactory.FindByServiceName("BoP/Core/Domain/IPersonManager");
            return um.UpdatePerson(newPerson, originalPerson);
        }

    }
}
