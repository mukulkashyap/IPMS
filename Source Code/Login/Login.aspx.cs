/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using IPMS.Helper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPMS.Login
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Request.QueryString.Keys.Count > 0 && Convert.ToString(Request.QueryString["q"]).ToUpper() == "V")
            {
                divVerify.Visible = true;
            }
            if (Convert.ToString(hidLogin.Value).ToUpper() == "TRUE")
            {
                if (Session["user"]==null)
                {
                    SendEmail();
                    Response.Redirect("Login.aspx?q=v");
                }
                else
                {
                    Response.Redirect("../Activity/Activity");
                }
            }
        }

        /// <summary>
        /// Ajax Call Method
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static bool SubmitMe(string userEmail, string password)
        {
            bool isLoginValid = true;
            clsLogin obj = new clsLogin();
            Login This = new Login();      
            isLoginValid = obj.Login(userEmail, password);            
            return isLoginValid;


        }
        /// <summary>
        /// Send the email about verification link
        /// </summary>
        public void SendEmail()
        {
            User user = new User();
            user = (User)Session["user"];

            MailModel mailmodel = new MailModel();
            clsCommonFunction objCommonFncs = new clsCommonFunction();
            clsEmailFncs objEmail = new clsEmailFncs();
            StringBuilder strMessage = new StringBuilder();
            string confirmLink = string.Empty;
            DataSet ds = new DataSet();
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.UserPasswordDetail);
            cmd.Parameters.AddWithValue("@login_id", loginEmail.Value);
            cmd.CommandType = CommandType.StoredProcedure;
            ds = dbConnect.ExecuteDataSet(cmd);
            confirmLink = objCommonFncs.getValueForColumn(ds, "confirm_link");
            string pagePath = objCommonFncs.GetKeyValuePairValue("host");
            pagePath = pagePath + "/Login/confirm.aspx?" + confirmLink;

            strMessage.Append("Hello ").AppendLine("</b><br/><br/>");
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

                objCommonFncs.getEmailAttendees(loginEmail.Value, "", mailmodel);
                mailmodel.Subject = "IPMS Email Verification";
                mailmodel.MailBody = strMessage.ToString();

                objEmail.SendEmailFncs(mailmodel);
            }
            catch (Exception ex)
            {
                Log.Error("Error while sending the verification link!!!" + ex.ToString());
            }

        }

        [System.Web.Services.WebMethod]
        public static bool SendForgotPassowrdMail(string userEmail)
        {
            bool isUserAlreadyRegistered = false;
            clsCommonFunction objCmnFncs = new clsCommonFunction();

            string userName = string.Empty;
            userName = objCmnFncs.getFirstNameForEmail(userEmail);

            if (!String.IsNullOrWhiteSpace(userName))
            {
                SendPasswordinMail(userName, userEmail);
                isUserAlreadyRegistered = true;
            }
            return isUserAlreadyRegistered;

        }

        public static void SendPasswordinMail(string userName, string userEmail)
        {
            MailModel mailmodel = new MailModel();
            clsCommonFunction objCmnFncs = new clsCommonFunction();
            clsEmailFncs objEmailFncs = new clsEmailFncs();
            StringBuilder strUserMessage = new StringBuilder();

            string NewPassword = string.Empty;
            NewPassword = objCmnFncs.getNewPasswordforUser();

            strUserMessage.Append("Dear " + userName).AppendLine("<br/><br/><br/>");
            strUserMessage.Append("We have received a request for resetting your login credentials.").AppendLine("<br/><br/>");
            strUserMessage.Append("Please find below your new password to login to IPMS.").AppendLine("<br/><br/>");
            strUserMessage.Append("Password : <b>" + NewPassword).AppendLine("</b><br/><br/>");
            strUserMessage.Append("Please login to IPMS using the new password and then you can change the password from the 'Change Password' option.").AppendLine("<br/><br/>");
            strUserMessage.Append("Ignore this mail if it was not initiated by you.").AppendLine("<br/><br/>");
            strUserMessage.Append("Best Regards").AppendLine("<br/>").Append("<b>The IPMS Team</b>").AppendLine("<br/><br/>").AppendLine("<br/><br/>");

            try
            {
                mailmodel.From = new EmailAddress
                {
                    Address = objCmnFncs.getValueFromConfig("IPMSAdminEmail"),
                    Name = objCmnFncs.getValueFromConfig("IPMSAdminDisplayName")
                };

                objCmnFncs.getEmailAttendees(userEmail, "", mailmodel);
                mailmodel.Subject = "Forgot Password";
                mailmodel.MailBody = strUserMessage.ToString();

                objEmailFncs.SendEmailFncs(mailmodel);
                UpdateDBwithNewPassword(userEmail, NewPassword);
            }
            catch (Exception ex)
            {
                Log.Error("Error in SendPasswordinMail !!! Email to: " + Convert.ToString(mailmodel.To[0]) + "; Error Subject: " + mailmodel.Subject + "; Error Message: " + ex.ToString());
            }
        }

        public static void UpdateDBwithNewPassword(string userEmail, string newPassword)
        {
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.UpdateUserPasswordInfo);
            cmd.Parameters.AddWithValue("@login_id", userEmail);
            cmd.Parameters.AddWithValue("@pswd", EncrypterDecrypter.Encrypt(Convert.ToString(newPassword)));
            cmd.CommandType = CommandType.StoredProcedure;
            dbConnect.ExecuteNonQuery(cmd);
            
        }


    }
}