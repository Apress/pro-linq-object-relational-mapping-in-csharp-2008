using System;
using System.Collections.Generic;
using System.Text;

namespace BoP.Core.Domain
{
    [Serializable]
    public class LoanApplication: Account
    {
        public int RequestedAmount { get; set; }
        public string LoanPurpose { get; set; }
        public bool Approval { get; set; }
        public int CreditScore { get; set; }

    }


}
