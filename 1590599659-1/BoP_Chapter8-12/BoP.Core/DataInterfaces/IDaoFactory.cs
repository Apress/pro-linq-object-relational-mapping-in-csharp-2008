using BoP.Core;
using BoP.Core.Domain;

namespace BoP.Core.DataInterfaces
{
    /// <summary>
    /// This interface was adapted from Billy Mccafferty's NHibernate Framework
    /// <see cref="http://devlicio.us/blogs/billy_mccafferty"/>
    /// 
    /// The purpose of this interface is to provide 
    /// the contracts for retrieving DAO objects
    /// in a decoupled manner
    /// </summary>
    
    public interface IDaoFactory 
    {
        IAccountDao GetAccountDao();
        ICreditHistoryDao GetCreditHistoryDao();
        ILoanDao GetLoanDao();
        ILoanApplicationDao GetLoanApplicationDao();
        IUserDao GetUserDao();
        IPersonDao GetPersonDao();
        IApplicantDao GetApplicantDao();

    }

    #region Inline interface declarations

    public interface IAccountDao : IDao<Account, int> { }
    public interface ILoanDao : IDao<Loan, int> { }
    public interface ILoanApplicationDao : IDao<LoanApplication, int> { }
    public interface ICreditHistoryDao : IDao<CreditHistory, int> { }
    public interface IApplicantDao : IDao<Applicant, int> { }

    #endregion
}
