using BoP.Core.DataInterfaces;
using BoP.Core.Domain;
using BoP.Core;

namespace BoP.Data.LTS
{
    /// <summary>
    /// Exposes access to LTS DAO classes.  This framework 
    /// was adapted from Billy Mccafferty's NHibernate Framework.
    /// 
    /// This is the concrete implementation of IDaoFactory which
    /// is exposed from the BoP.Core.DataInterfaces domain model
    /// </summary>
    public class LTSDaoFactory : IDaoFactory
    {

        #region Inline DAO implementations

        public class AccountDao : AbstractLTSDao<Account, int>, IAccountDao{ }

        #endregion

        #region IDaoFactory Members

        public IAccountDao GetAccountDao()
        {
            return new AccountDao();
        }

        public IPersonDao GetPersonDao()
        {
            return new PersonDao();
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
            return new LoanApplicationDao();
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
