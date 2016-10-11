using System;
using System.Collections.Generic;
using System.Text;

namespace BoP.Core.Domain
{
    /// <summary>
    /// The Applicant class is a Role in the system and is a 
    /// transient object. A Person in the system is only 
    /// temporarily an Applicant because they become a Borrower
    /// after their application is approved.
    /// </summary>
    public class Applicant:Role
    {
        private const string ROLE_NAME = "Applicant";

        public Applicant() : base(ROLE_NAME) { }

        public LoanApplication LoanApp { get; set; }
    }
}
