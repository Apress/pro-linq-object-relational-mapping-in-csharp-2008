using System;
using System.Collections.Generic;
using System.Text;

namespace BoP.Core.Domain
{
    [Serializable]
    public class Borrower : Applicant
    {
        private const string ROLE_NAME = "Borrower";

        public Borrower() { }

        public IList<Loan> Loan { get; set; }

    }
}
