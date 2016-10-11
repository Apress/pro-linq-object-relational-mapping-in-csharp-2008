using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using BoP.Core;
using BoP.Data;
using NUnit.Framework;
using BoP.Core.Domain;
using BoP.Util;
using BoP.Data.LTS;
using BoP.Core.DataInterfaces;

namespace BoP.Tests
{
    [TestFixture]
    public class UseCaseTests
    {
        IServiceFactory serviceFactory;
        private const string USERMAN = "BoP/Core/Domain/IUserManager";
        
        [Test]
        public void CreateUser()
        {
            serviceFactory = new ClassServiceFactory();
            IUserManager um = (IUserManager)serviceFactory.FindByServiceName(USERMAN);

            User testUser = new User();
            testUser.Person = new Person { Email = "testPerson@bop.com" };
            testUser.Active = 'T';
            testUser.UserId = "testUser";
            testUser.Password = "testPassword";
            um.CreateUser(testUser);

            Assert.Greater(testUser.Person.StakeHolderId,0);

        }


        [Test]
        public void AuthenticateUser()
        {
            serviceFactory = new ClassServiceFactory();
            IUserManager um = (IUserManager)serviceFactory.FindByServiceName(USERMAN);

            User testUser = um.Authenticate("testUser", "testPassword");
            Assert.IsNotNull(testUser);

        }

        [Test]
        public void CreateLoanApplication()
        {
            serviceFactory = new ClassServiceFactory();
            ILoanApplicationManager um = (ILoanApplicationManager)serviceFactory.FindByServiceName("BoP/Core/Domain/ILoanApplicationManager");
            LoanApplication la = new LoanApplication();
            la.LoanPurpose = "Building a bridge to no where";
            la.RequestedAmount = 1000000;
            //Hardcode to a Person for testing -> you can always change
            // this to retrieve a "testPerson" if you like
            la.StakeHolderId = 1;
            um.SaveLoanApplication(la);
            Assert.IsNotEmpty(la.ID.ToString());
        }

        [Test]
        public void GetLoanAppsByTaxId()
        {
            serviceFactory = new ClassServiceFactory();
            IPersonManager um = (IPersonManager)serviceFactory.FindByServiceName("BoP/Core/Domain/IPersonManager");
            Person p = um.GetPersonByTaxId("123-12-1234");
            var x = from s in p.Roles
                    select s;

            foreach (User r in x)
            {
                Assert.IsNotEmpty(r.UserId);
            
            }


        }


        [Test]
        public void GetLoanAppAndUpdateDisconnected()
        {
            serviceFactory = new ClassServiceFactory();
            IPersonManager um = (IPersonManager)serviceFactory.FindByServiceName("BoP/Core/Domain/IPersonManager");
            Person p = um.GetPersonByTaxId("123-12-1234");
            var x = from s in p.Roles
                    select s;

            foreach (User r in x)
            {
                Assert.IsNotEmpty(r.UserId);

            }


        }
 
    }
}
