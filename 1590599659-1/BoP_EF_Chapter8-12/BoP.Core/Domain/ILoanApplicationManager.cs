using System;
using System.Collections.Generic;
using System.Text;

namespace BoP.Core.Domain
{
    public interface ILoanApplicationManager
    {
        LoanApplication SaveLoanApplication(LoanApplication la);

        LoanApplication DecisionLoanApplication(LoanApplication la);

        LoanApplication GetCreditHistory(LoanApplication la);

    }
}
