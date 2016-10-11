using System;
using System.Collections.Generic;
using System.Text;

namespace BoP.Core.Domain
{
    /// <summary>
    /// Provides additional operations that are not defined 
    /// as creational or query methods in the DAO layer.  
    /// </summary>
    public interface IApplicantManager
    {

        /// <summary> 
        /// A method that populates the credit 
        /// history on a loan application
        /// credit score.
        /// </summary>
        /// 

        LoanApplication GetCreditHistory(LoanApplication la);


    }
}
