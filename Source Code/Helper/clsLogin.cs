/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace IPMS.Helper
{
    public class clsLogin
    {
        public string UserStatus { get; set; }
        /// <summary>
        /// Main function for login
        /// </summary>
        /// <param name="loginUsername"></param>
        /// <param name="loginPassword"></param>
        /// <returns></returns>
        public bool Login(string loginUsername, string loginPassword)
        {
            bool isPasswordCorrect = false;
            DataSet ds = new DataSet();
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.UserPasswordDetail);
            cmd.Parameters.AddWithValue("@login_id", loginUsername);
            cmd.CommandType = CommandType.StoredProcedure;
            ds = dbConnect.ExecuteDataSet(cmd);
            UserStatus = "X";
            if (checkPassword(ds, loginPassword))
            {
                isPasswordCorrect = true;

            }
            else
            {

            }
            return isPasswordCorrect;
        }
        /// <summary>
        /// Check Password
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private bool checkPassword(DataSet ds, string loginPassword)
        {
            clsCommonFunction cmnFncs = new clsCommonFunction();
            string id = string.Empty;
            string email = string.Empty;
            string userType = string.Empty;
            string decryptedPswd = string.Empty;
            bool isValidUser = false;
            UserStatus = "O";
            string password = cmnFncs.getValueForColumn(ds, "pswd");
            try
            {
                decryptedPswd = EncrypterDecrypter.Decrypt(password);
            }
            catch (Exception e)
            {
                Log.Error("Error While Decryoting password " + password + " Error Is " + e.ToString());
            }

            if (loginPassword == decryptedPswd)
            {
                UserStatus = cmnFncs.getValueForColumn(ds, "user_status").ToUpper();
                isValidUser = true;
                if (UserStatus == "A")
                {
                    try
                    {
                        id = cmnFncs.getValueForColumn(ds, "userid");
                        email = cmnFncs.getValueForColumn(ds, "login_id");
                        userType = cmnFncs.getValueForColumn(ds, "user_type");
                        SetSession(id, email, userType);
                        
                    }
                    catch (Exception e)
                    {
                        Log.Message(id, "Error Happened while creating Session for ID -  " + id + " email - " + email + " -------------------  " + e.ToString());
                    }
                }

            }

            return isValidUser;
        }

        /// <summary>
        /// Setting the session variable.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="usertype"></param>
        private void SetSession(string id, string email, string usertype)
        {
            clsCommonFunction cmnFncs = new clsCommonFunction();
            User user = new User();
            user.ID = id;
            user.Email = email;
            user.FirstName = cmnFncs.getFirstNameForEmail(email);
            user.UserType = usertype;
            HttpContext.Current.Session["user"] = user;
            
        }
    }
}