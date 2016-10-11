using System;
using System.Collections.Generic;

namespace BoP.Core.Domain
{
    /// <summary>
    /// The account class is a general use object in the system.
    /// This class is instantiated when a persons loan is accepted,
    /// and is also sub-classed for more granular control (e.g. Loan).
    /// </summary>
    /// 
    [Serializable]
    public class Account:BaseEntity<int>
    {
        public Account() 
        {
        
            
        }

        public override int ID
        {
            get
            {
                return base.ID;
            }
             set
            {
                base.ID = value;
            }
        }

        public int StakeHolderId { get; set; }
        public StakeHolder StakeHolder{ get; set; }

        public virtual Person Person
        {
            get
            {
                if (StakeHolder is Person)
                    return ((Person)StakeHolder);
                else
                    return (null);
            }

            set
            {
                StakeHolder = value;
            }
        }

        
        public decimal Balance { get; set; }
        public AccountType AccountType{ get; set; }

    }

    public enum AccountType: int 
    {
        Account = 0,
        Loan = 1

    }
}
