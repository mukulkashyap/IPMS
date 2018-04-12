/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using System;
using IPMS.Helper;
using System.Data.SqlClient;
using System.Data;

namespace IPMS.Login
{
    public partial class Confirm : System.Web.UI.Page
    {
        string userName, password;
        protected void Page_Load(object sender, EventArgs e)
        {

            clsLogin objLogin = new clsLogin();
            bool isLoginSuccess = false;            
            clsCommonFunction obj = new clsCommonFunction();
            if (Request.QueryString.Keys.Count > 0)
            {
                try
                {
                    string confirmLink = Request.QueryString["q"];
                    confirmLink = "q=" + confirmLink;
                    setValues(confirmLink);
                    if (string.IsNullOrWhiteSpace(Convert.ToString(userName)) || string.IsNullOrWhiteSpace(Convert.ToString(password)))
                    {
                        Response.Write("The Link is expired. Please try forgot password.");
                        
                    }
                    else
                    {
                        updateUserAccess(userName);
                        isLoginSuccess = objLogin.Login(userName, EncrypterDecrypter.Decrypt(password));
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Oops Something went wrong. Please contact admin");
                    Log.Error("Error while confirming email" + ex.ToString());
                }
                if (!isLoginSuccess)
                {
                    Response.Write("Please contact Admin as the url is not working any more");
                }
                else
                {

                    obj.InsertActivity(userName, " has <span class='label label-success'>joined</span> <b>IPMS</b>","J");
                    Log.Activity( DateTime.Now.ToString() + "User is confirmed his email verification" + userName );
                    Response.Redirect("../Home/UserDetail");

                }
            }


        }

        /// <summary>
        /// set the values of data from DB
        /// </summary>
        /// <param name="confirmLink"></param>
        private void setValues(string confirmLink)
        {
            clsCommonFunction obj = new clsCommonFunction();
            DataSet ds = new DataSet();
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.SelectUserDetailWithConfirmLink);
            cmd.Parameters.AddWithValue("@confirm_link", confirmLink);
            cmd.CommandType = CommandType.StoredProcedure;
            ds = dbConnect.ExecuteDataSet(cmd);
            userName = obj.getValueForColumn(ds, "login_id");
            password = obj.getValueForColumn(ds, "pswd");
        }

        /// <summary>
        /// Update the access from P to A - Pending to Active
        /// </summary>
        /// <param name="email"></param>
        private void updateUserAccess(string email)
        {
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.UpdateUserStatus);
            cmd.Parameters.AddWithValue("@login_id", email);
            cmd.Parameters.AddWithValue("@user_status", "A");
            cmd.CommandType = CommandType.StoredProcedure;
            dbConnect.ExecuteNonQuery(cmd);
        }
    }
}