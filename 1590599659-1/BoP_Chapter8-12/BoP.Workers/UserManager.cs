using System;
using System.Collections.Generic;
using System.Text;
using BoP.Core;
using BoP.Core.Domain;
using BoP.Core.DataInterfaces;
using BoP.Util;

namespace BoP.Workers
{
    public class UserManager: IUserManager
    {

        #region IUserManager Members

        public User Authenticate(string userName, string password)
        {
            try
            {
                IServiceFactory serviceFactory = new ClassServiceFactory();
                IDaoFactory df = (IDaoFactory)serviceFactory.FindByServiceName("BoP/Core/DataInterfaces/IDaoFactory");
                IUserDao iud = df.GetUserDao();

                User x = iud.GetByUserIdAndPassword(userName, password);

   
                if (x.Active == 'F')
                    throw new UserDisabledException("Your Account Has Been Disabled.  Please Contact Customer Service.");


                return x;

            }
            catch (InvalidOperationException ioe)
            {
                
                throw new InvalidUserNameOrPassword("You have entered an incorrect username or password.  Please contact Customer Service.",ioe);
                
            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }

        public User CreateUser(User user)
        {
            IServiceFactory serviceFactory = new ClassServiceFactory();
            IDaoFactory df = (IDaoFactory)serviceFactory.FindByServiceName("BoP/Core/DataInterfaces/IDaoFactory");
            IPersonDao ipd = df.GetPersonDao();

            Person testPerson = user.Person;
            ipd.Save(testPerson);
            testPerson.AddRole(user);
            ipd.CommitChanges();
            return user;
            
        }

        public User UpdateProfile(User user, string newPassword, string newChallengeQuestion, string newChallengeAnswer)
        {
            throw new NotImplementedException();
        }

        public User UpdatePassword(User user, string newPassword)
        {
            throw new NotImplementedException();
        }

        public string[] GetChallengeQuestions()
        {
            throw new NotImplementedException();
        }

        public User GrantAuthenticationLevel(User user, IAuthenticationLevel level)
        {
            throw new NotImplementedException();
        }

        public void Logout(User user)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IUserManager Members


        public string ForgotPassword(string email)
        {
            throw new NotImplementedException();
        }

        public string ForgotUserName(string email)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
