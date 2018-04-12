﻿/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using System;

namespace IPMS.Login
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            clearValue();
            Response.Redirect("../Home/Home");
        }

        /// <summary>
        /// clear the session.
        /// </summary>
        private void clearValue()
        {
            if (Session != null)
            {
                Session.Clear();
            }
        }
    }
}