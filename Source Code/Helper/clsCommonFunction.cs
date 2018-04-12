/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace IPMS.Helper
{
    public class clsCommonFunction
    {


        /// <summary>
        /// Retursn the first name of the user
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public string getFirstNameForEmail(String Email)
        {
            DataSet ds = new DataSet();
            clsDbConnect dbConnect = new clsDbConnect();
            string name = string.Empty;
            SqlCommand cmd = new SqlCommand(SPList.UserDetail);
            cmd.Parameters.AddWithValue("@strEmailId", Email);
            cmd.CommandType = CommandType.StoredProcedure;
            ds = dbConnect.ExecuteDataSet(cmd);
            return getValueForColumn(ds, "full_name");
        }


        /// <summary>
        /// Return the value of column
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public string getValueForColumn(DataSet ds, string column)
        {
            string value = string.Empty;
            if (ds != null)
            {

                if (ds.Tables != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        value = Convert.ToString(ds.Tables[0].Rows[0][column]);

                    }
                }
            }
            return value;
        }
        /// <summary>
        /// To Set the dropdown
        /// </summary>
        /// <param name="dropDown"></param>
        /// <param name="sql"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public DataTable GetKeyValuePairDetail(string grpKey)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.KeyValuePairDetail);
            cmd.Parameters.AddWithValue("@strGrpDetail", grpKey);
            cmd.CommandType = CommandType.StoredProcedure;
            ds = dbConnect.ExecuteDataSet(cmd);
            dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// Returns the value from key value pair table
        /// </summary>
        /// <param name="grpKey"></param>
        /// <returns></returns>
        public string GetKeyValuePairValue(string grpKey)
        {

            DataTable dt = new DataTable();
            string value = string.Empty;
            dt = GetKeyValuePairDetail(grpKey);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    value = Convert.ToString(dt.Rows[0]["grp_val"]);
                }
            }

            return value;
        }


        /// <summary>
        /// return the value from web config for a specific key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string getValueFromConfig(string key)
        {
            string value = string.Empty;


            try
            {
                value = System.Configuration.ConfigurationManager.AppSettings[key].ToString();
            }
            catch (Exception e)
            {
                Log.Error("Error While Fetching Config Value for Key " + key + " and the error id " + e.ToString());
            }
            return value;
        }

        /// <summary>
        /// Insert the activity in the table 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="activity"></param>
        public void InsertActivity(string email, string activity, string activityType)
        {
            Log.Activity(email + " " + activity + " On " + DateTime.Now.ToShortDateString());
            DataTable dt = new DataTable();
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.InsertActivity);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@activity", activity);
            cmd.Parameters.AddWithValue("@activity_type", activityType);
            cmd.CommandType = CommandType.StoredProcedure;
            dbConnect.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Returns the actual path to save
        /// </summary>
        /// <param name="FileFolder"></param>
        /// <returns></returns>
        public string GetFilePathToSave(string FileFolderKeyName)
        {
            string completePath = string.Empty;
            completePath = GetKeyValuePairValue("FileActualPath");
            completePath = completePath + "\\" + GetKeyValuePairValue(FileFolderKeyName) + "\\";
            if (!Directory.Exists(completePath))
            {
                Directory.CreateDirectory(completePath);
            }
            return completePath;
        }
        /// <summary>
        /// Return the file path to show info
        /// </summary>
        /// <param name="FileFolderKeyName"></param>
        /// <returns></returns>
        public string GetFilePathToShow(string FileFolderKeyName)
        {
            string completePath = string.Empty;
            completePath = GetKeyValuePairValue("FileVirtualPath");
            completePath = completePath + "/" + GetKeyValuePairValue(FileFolderKeyName) + "/";
            return completePath;
        }

        /// <summary>
        /// Provide the image url to show
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetImageURL(User user)
        {
            string imageUrl = string.Empty;
            string imageName = string.Empty;
            DataSet ds = new DataSet();
            clsDbConnect dbConnect = new clsDbConnect();
            string name = string.Empty;
            SqlCommand cmd = new SqlCommand(SPList.UserDetail);
            cmd.Parameters.AddWithValue("@strEmailId", user.Email);
            cmd.CommandType = CommandType.StoredProcedure;
            ds = dbConnect.ExecuteDataSet(cmd);
            imageName = getValueForColumn(ds, "image_name");
            if (imageName == "")
            {
                if (user.FirstName.Trim() != "")
                {
                    imageUrl = GetKeyValuePairValue("host");
                    imageUrl = imageUrl + "content/img/portfolio/";
                    imageUrl = imageUrl + user.FirstName.Trim().Substring(0, 1).ToUpper();
                    imageUrl = imageUrl + ".png";
                }
            }
            else
            {
                imageUrl = GetFilePathToShow("ImageFolder");
                imageUrl = imageUrl + imageName;
            }
            return imageUrl;
        }

        public MailModel getEmailAttendees(string attendeeNamesinTo, string attendeeNamesinCC, MailModel mailObject)
        {
            try
            {
                char[] delimiter = new char[] { ';' };
                List<EmailAddress> toEmailList = new List<EmailAddress>();
                List<EmailAddress> ccEmailList = new List<EmailAddress>();
                string[] attendeesListinTo = attendeeNamesinTo.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                if (attendeeNamesinTo != null && attendeesListinTo.Length > 0)
                {
                    for (int i = 0; i < attendeesListinTo.Length; i++)
                    {
                        toEmailList.Add(new EmailAddress { Address = attendeesListinTo[i] });
                    }

                }
                mailObject.To = toEmailList;
                string[] attendeesListinCC = attendeeNamesinCC.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                if (attendeesListinCC != null && attendeesListinCC.Length > 0)
                {
                    for (int i = 0; i < attendeesListinCC.Length; i++)
                    {
                        ccEmailList.Add(new EmailAddress { Address = attendeesListinCC[i] });
                    }

                }
                mailObject.Cc = ccEmailList;
            }
            catch (Exception ex)
            {
                Log.Error("Error in getting Email Attendee list!! Error Message: " + ex.Message);
            }
            return mailObject;
        }

        public string getNewPasswordforUser()
        {
            clsCreatePassword objNewPassword = new clsCreatePassword();
            string newPassword = string.Empty;

            newPassword = objNewPassword.Generate();
            return newPassword;
        }
    }
}