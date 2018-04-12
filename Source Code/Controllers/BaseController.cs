/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using IPMS.Helper;
using System.Web.Mvc;

namespace IPMS.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnAuthorization(AuthorizationContext context)
        {

            CheckSecurity();
        }


        /// <summary>
        /// Check The Important Security Check
        /// </summary>
        private void CheckSecurity()
        {
            User user = new User();
            user = (User)Session["user"];
            if (!clsCheckSecurity.securityCheck(user))
            {
                Response.Redirect("../Login/Logout.aspx");
            }
        }
    }
}