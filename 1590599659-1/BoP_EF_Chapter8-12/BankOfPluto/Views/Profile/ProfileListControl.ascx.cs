using System;
using System.Web;
using System.Web.Mvc;
using BoP.Core.Domain;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace BankOfPluto.Views.Profile
{
    public partial class ProfileListControl : System.Web.Mvc.ViewUserControl<Person>
    {

        protected DataList profileDataList;

        private void Page_Load(object sender, System.EventArgs e)
        {
            List<Person> lp = new List<Person>();
            lp.Add(ViewData);
            
            profileDataList.DataSource = lp;
            profileDataList.DataBind();

        }


    }
}