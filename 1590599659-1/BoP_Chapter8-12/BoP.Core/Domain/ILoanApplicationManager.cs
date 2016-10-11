using System;
using System.Collections.Generic;
using System.Text;

namespace BoP.Core.Domain
{
    /// <summary>
    /// The ILoanApplicationManager Interface is used to define
    /// the contract for creating a new LoanApplicationManager
    /// concrete class.  This class provides the necessary "worker"
    /// functions for manipulating a LoanApplication
    /// </summary>
    public interface ILoanApplicationManager
    {

        /// <summary> 
        /// A method that saves the 
        /// loan application before or after
        /// decisioning and credit history
        /// </summary>
        /// 
        /// <param name="la">
        /// Loan Application
        /// </param>
        /// 
        /// <returns>
        /// Loan Application
        /// </returns>
        /// 
        /// <exception cref="BoP.Core.SaveLoanException">
        /// an unexpected exception occurs
        /// </exception>
        LoanApplication SaveLoanApplication(LoanApplication la);

        /// <summary> 
        /// A method that reviews the 
        /// loan application and determines
        /// if the applicant is worthy of the loan
        /// </summary>
        /// 
        /// <param name="la">
        /// Loan Application
        /// </param>
        /// 
        /// <returns>
        /// Loan Application
        /// </returns>
        /// 
        /// <exception cref="BoP.Core.DecisionLoanException">
        /// an unexpected exception occurs
        /// </exception>
        LoanApplication DecisionLoanApplication(LoanApplication la);

        /// <summary> 
        /// A method that populates the credit 
        /// history on a loan application
        /// credit score.
        /// </summary>
        /// 
        /// <param name="la">
        /// Loan Application
        /// </param>
        /// 
        /// <returns>
        /// Loan Application
        /// </returns>
        /// 
        /// <exception cref="BoP.Core.CreditHistoryException">
        /// an unexpected exception occurs
        /// </exception>
        /// 
        LoanApplication GetCreditHistory(LoanApplication la);

    }
}
