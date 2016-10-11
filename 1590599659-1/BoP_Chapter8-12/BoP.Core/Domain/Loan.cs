using System;
using System.Collections.Generic;
using System.Text;

namespace BoP.Core.Domain
{
    /// <summary>
    /// The Loan class is a subclass of Account.
    /// This class contains loan specific information
    /// and is created after an application is approved.
    /// </summary>
    public class Loan:Account
    {

        public decimal? InterestRate { get; set; }
        public int? Term{ get; set; }
        public decimal? Principal { get; set; }
    }
}
