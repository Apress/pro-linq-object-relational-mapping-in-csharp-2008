using BoP.Core.DataInterfaces;
using BoP.Core.Domain;
using BoP.Core;

namespace BoP.Data.EF
{
    /// <summary>
    /// Exposes access to EF DAO classes.  This framework 
    /// was adapted from Billy Mccafferty's NHibernate Framework.
    /// 
    /// This is the concrete implementation of IDaoFactory which
    /// is exposed from the BoP.Core.DataInterfaces domain model
    /// </summary>
    public class EFDaoFactory : IDaoFactory
    {

        #region Inline DAO implementations

        public class AccountDao : AbstractEFDao<Account, int>, IAccountDao { }

        #endregion

        #region IDaoFactory Members

        public IAccountDao GetAccountDao()
        {
            return new AccountDao();
        }

        public IPersonDao GetPersonDao()
        {
            return new PersonEFDao();
        }


        public ICreditHistoryDao GetCreditHistoryDao()
        {
            throw new System.NotImplementedException();
        }

        public ILoanDao GetLoanDao()
        {
            throw new System.NotImplementedException();
        }

        public ILoanApplicationDao GetLoanApplicationDao()
        {
            throw new System.NotImplementedException();
            //return new ILoanApplicationDao();
        }

        public IUserDao GetUserDao()
        {
            return new UserDao();
        }


        public IApplicantDao GetApplicantDao()
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}
