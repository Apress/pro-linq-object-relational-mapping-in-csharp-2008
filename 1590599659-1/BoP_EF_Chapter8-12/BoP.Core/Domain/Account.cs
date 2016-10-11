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
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName = "BoP.Core.Domain", Name = "Account")]
    public class Account:BaseEntity<int>
    {
        private decimal _balance;
        private int _stakeHolderId;
        private StakeHolder _stakeHolder;
        private Person _person;

        public Account() 
        {         
        }

        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        public int AccountNumber
        {
            get
            {
                return base.ID;
            }
            set
            {
                this.PropertyChanging("ID");
                base.ID = value;
                this.PropertyChanging("ID");
            }
        }

        
        public int StakeHolderId 
        { 
            get
            {
                return _stakeHolderId;
            }
            set
            {
                this.PropertyChanging("StakeHolderId");
                _stakeHolderId = value;
                this.PropertyChanging("StakeHolderId");

                        
            }
        
        }


        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("BoP.Core.Domain", "StakeHolderAccount", "StakeHolder")]
        public StakeHolder StakeHolder
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<StakeHolder>("BoP.Core.Domain.StakeHolderAccount", "StakeHolder").Value;
            }
            set
            {
                ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<StakeHolder>("BoP.Core.Domain.StakeHolderAccount", "StakeHolder").Value = value;
            }
        }
        /// <summary>
        /// There are no comments for StakeHolder in the schema.
        /// </summary>
        public global::System.Data.Objects.DataClasses.EntityReference<StakeHolder> StakeHolderReference
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<StakeHolder>("BoP.Core.Domain.StakeHolderAccount", "StakeHolder");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedReference<StakeHolder>("BoP.Core.Domain.StakeHolderAccount", "StakeHolder", value);
                }
            }
        }
    
        
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

        //public int AccountNumber { get; set; }
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        public decimal Balance 
        {
            get { return _balance;}
            set
            {
                this.PropertyChanging("Balance");
                _balance = value;
                this.PropertyChanged("Balance");

            }
        
        }
        public AccountType AccountType{ get; set; }

    }

    public enum AccountType: int 
    {
        Account = 0,
        Loan = 1

    }
}
