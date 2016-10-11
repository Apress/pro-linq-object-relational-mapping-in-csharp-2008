using System;
using System.Collections.Generic;
using System.Text;
using BoP.Core.Domain;
using BoP.Core.DataInterfaces;
using BoP.Util;

namespace BoP.Workers
{
    public class LoanApplicationManager : ILoanApplicationManager
    {
        #region ILoanApplicationManager Members

        public LoanApplication SaveLoanApplication(LoanApplication la)
        {
            IServiceFactory serviceFactory = new ClassServiceFactory();
            IDaoFactory df = (IDaoFactory)serviceFactory.FindByServiceName("BoP/Core/DataInterfaces/IDaoFactory");
            ILoanApplicationDao ild = df.GetLoanApplicationDao();
            ild.Save(la);
            return la;
            
        }

        public LoanApplication GetCreditHistory(LoanApplication la)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ILoanApplicationManager Members




        #endregion

        #region ILoanApplicationManager Members


        public LoanApplication DecisionLoanApplication(LoanApplication la)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
