/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using System;
using IPMS.Helper;

namespace PatchMaintainence.Home
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static bool SubmitMessage(string userName, string userEmail, string userMessage)
        {
            bool sendMessage = false;
            MailModel mailmodel = new MailModel();
            clsCommonFunction objCmnFncs = new clsCommonFunction();
            clsEmailFncs objEmailFncs = new clsEmailFncs();
            try
            {
                mailmodel.From = new EmailAddress {
                    Address = userEmail,
                Name = userName };

                objCmnFncs.getEmailAttendees(objCmnFncs.getValueFromConfig("IPMSGroup"), "", mailmodel);
                mailmodel.Subject = objCmnFncs.getValueFromConfig("AskMoreSubject");
                mailmodel.MailBody = userMessage;

                objEmailFncs.SendEmailFncs(mailmodel);
                sendMessage = true;
            }
            catch (Exception ex)
            {
                Log.Error("Error in SubmitMessage !!! Email From: " + Convert.ToString(mailmodel.From.Address) + "; Error Message: " + ex.ToString());
                sendMessage = false;
            }

            return sendMessage;
        }

    }
}