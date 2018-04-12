/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using IPMS.Helper;

namespace IPMS.Home
{
    public partial class Base : System.Web.UI.Page
    {


        //public Base()
        //{
        //    try
        //    {
        //        CheckSecurity();
        //    }
        //    catch ( Exception e)
        //    {
        //        Log.Message("9999", "Error Occured While Loading Base Class" + e.ToString());
        //        Response.Redirect("../Error/Error");
        //    }
        //}

        /// <summary>
        /// Check The Important Security Check
        /// </summary>
        private void CheckSecurity()
        {
            User user = new User();
            user = (User)Session["user"];
            if (!clsCheckSecurity.securityCheck(user))
            {
                Response.Redirect("../Login/Logout");
            }
        }

        public void SecurityCheck()
        {

            CheckSecurity();

        }
    }
}