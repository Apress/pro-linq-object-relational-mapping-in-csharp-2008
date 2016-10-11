using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using localhost;
using BoP.Core;
using BoP.Core.Domain;
using BoP.Util;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //Super simple test to test the service layer and the DTOHelper
    protected void Button1_Click(object sender, EventArgs e)
    {

        ManagerServicesClient msc = new ManagerServicesClient();
        Person origP = msc.GetPersonByTaxId("123-12-1234");
        Person newP = new DTOHelper<Person>().Clone(origP);
        newP.Email = "foo@bar.com";
        Response.Write(msc.UpdatePerson(newP, origP).Email); ;
        msc.Close();

    }
}
