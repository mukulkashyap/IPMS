/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IPMS.Helper;

namespace IPMS.Login
{
    public partial class Register : System.Web.UI.Page
    {
        private string confirmLink = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void RegisterMe(Object sender, EventArgs e)
        {
            clsLogin objLogin = new clsLogin();
            try
            {
                SaveUserPasswordData();
                SaveUserPersonalInfo();
                SendConfirmationEmail();
                Response.Redirect("../Login/Login?q=v");
                Log.Activity(DateTime.Now.ToString() + "New user registered in the system with pending status" + registerEmail.Value);

            }
            catch (Exception ex)
            {
                Log.Error("Error Occured" + ex.ToString());
            }

        }

        /// <summary>
        /// Send the email for veriying the email.
        /// </summary>
        private void SendConfirmationEmail()
        {
            MailModel mailmodel = new MailModel();
            clsCommonFunction objCommonFncs = new clsCommonFunction();
            clsEmailFncs objEmail = new clsEmailFncs();
            StringBuilder strMessage = new StringBuilder();

            string pagePath = objCommonFncs.GetKeyValuePairValue("host");
            pagePath = pagePath + "/Login/confirm.aspx?" + confirmLink;

            strMessage.Append("Dear <b>" + registerName.Value).AppendLine("</b><br/><br/>");
            strMessage.Append("You are a registered user of IPMS.").AppendLine("<br/><br/>");
            strMessage.Append("<a href='" + pagePath + "'>Click here</a> to verify your email address now.").AppendLine("<br/><br/>");
            strMessage.Append("Welcome to IPMS").AppendLine("<br/><br/>");
            strMessage.Append("Best Regards,").AppendLine("<br/><br/>").Append("<b>IPMS Team</b>").AppendLine("<br/><br/>");

            try
            {
                mailmodel.From = new EmailAddress
                {
                    Address = objCommonFncs.getValueFromConfig("IPMSAdminEmail"),
                    Name = objCommonFncs.getValueFromConfig("IPMSAdminDisplayName")
                };

                objCommonFncs.getEmailAttendees(registerEmail.Value,"", mailmodel);
                mailmodel.Subject = "IPMS Email Verification";
                mailmodel.MailBody = strMessage.ToString();

                objEmail.SendEmailFncs(mailmodel);
            }
            catch (Exception ex)
            {
                Log.Error("Error while sending the verification link!!!" + ex.ToString());
            }
        }

        /// <summary>
        /// Genratest the page link for verifying the email account
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private string GenratePageLink(string email,string password)
        {
            clsCommonFunction objCommonFncs = new clsCommonFunction();
            string pagePath = objCommonFncs.GetKeyValuePairValue("host");
            pagePath = pagePath + "/Login/confirm.aspx?" + confirmLink;
            return pagePath;
        }

        /// <summary>
        /// Save the user password detail
        /// </summary>
        private void SaveUserPasswordData()
        {
            clsDbConnect dbConnect = new clsDbConnect();
            confirmLink = "q=" + Guid.NewGuid();
            SqlCommand cmd = new SqlCommand(SPList.InsertUserPasswordInfo);
            cmd.Parameters.AddWithValue("@login_id", registerEmail.Value.Trim());
            cmd.Parameters.AddWithValue("@pswd", EncrypterDecrypter.Encrypt(registerPassword.Value));
            cmd.Parameters.AddWithValue("@confirm_link", confirmLink);
            cmd.CommandType = CommandType.StoredProcedure;
            dbConnect.ExecuteNonQuery(cmd);

        }

        /// <summary>
        /// Save the personal info
        /// </summary>
        private void SaveUserPersonalInfo()
        {
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.InsertUserPersonalInfo);
            cmd.Parameters.AddWithValue("@email", registerEmail.Value.Trim());
            cmd.Parameters.AddWithValue("@full_name", registerName.Value.Trim());
            cmd.CommandType = CommandType.StoredProcedure;
            dbConnect.ExecuteNonQuery(cmd);

        }

        /// <summary>
        /// This is web method to check the same email exist
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]

        public static bool checkEmailExists(string email)
        {
            bool IsEmailIdExists = false;
            DataSet ds = new DataSet();
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.UserPasswordDetail);
            cmd.Parameters.AddWithValue("@login_id", email.Trim());
            cmd.CommandType = CommandType.StoredProcedure;
            ds = dbConnect.ExecuteDataSet(cmd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsEmailIdExists = true;
            }
            return IsEmailIdExists;
        }

    }
}