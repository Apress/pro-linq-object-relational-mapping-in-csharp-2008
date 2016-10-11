using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.BindingHelpers;
using BoP.Core.Domain;

namespace BankOfPluto.Controllers
{
    public class ProfileController : Controller
    {
        [ControllerAction]
        public void CustomerProfile(string id)
        {
            localhost.ManagerServicesClient msc = new BankOfPluto.localhost.ManagerServicesClient();
            Person p = msc.GetPersonByTaxId(id);
            msc.Close();

            RenderView("Edit", p);
        }


        [ControllerAction]
        public void UpdateProfile()
        {
            Person p = new Person();
            p.UpdateFrom(Request.Form);
            localhost.ManagerServicesClient msc = new BankOfPluto.localhost.ManagerServicesClient();
            msc.UpdatePerson(p, null);
            msc.Close();
            RenderView("List", p);
        }

    }
}
