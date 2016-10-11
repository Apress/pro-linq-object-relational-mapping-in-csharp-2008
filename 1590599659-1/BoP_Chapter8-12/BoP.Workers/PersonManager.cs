using System;
using System.Collections.Generic;
using System.Text;
using BoP.Core;
using BoP.Core.Domain;
using BoP.Core.DataInterfaces;
using BoP.Util;

namespace BoP.Workers
{
    public class PersonManager:IPersonManager
    {
        public Person GetPersonByTaxId(string taxId)
        {
            IServiceFactory serviceFactory = new ClassServiceFactory();
            IDaoFactory df = (IDaoFactory)serviceFactory.FindByServiceName("BoP/Core/DataInterfaces/IDaoFactory");
            IPersonDao ipd = df.GetPersonDao();

            return ipd.GetByTaxId(taxId);

        }

        public Person GetPersonByTaxIdDisconnected(string taxId)
        {
            IServiceFactory serviceFactory = new ClassServiceFactory();
            IDaoFactory df = (IDaoFactory)serviceFactory.FindByServiceName("BoP/Core/DataInterfaces/IDaoFactory");
            IPersonDao ipd = df.GetPersonDao();

            return ipd.GetByTaxId(taxId);

        }

        public Person UpdatePerson(Person newP, Person origP)
        {
            IServiceFactory serviceFactory = new ClassServiceFactory();
            IDaoFactory df = (IDaoFactory)serviceFactory.FindByServiceName("BoP/Core/DataInterfaces/IDaoFactory");
            IPersonDao ipd = df.GetPersonDao();
            return ipd.Update(newP,origP);

        }

    }
}
