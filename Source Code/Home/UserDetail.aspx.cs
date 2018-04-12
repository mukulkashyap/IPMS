/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IPMS.Helper;
using System.IO;

namespace IPMS.Home
{
    public partial class UserDetail : Base
    {
        User user = new User();
        clsCommonFunction cmnFncs = new clsCommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            SecurityCheck();
            user = (User)Session["user"];

            if ("Profile".Equals(Request.Form["hidSaveData"]))
            {
                UpdateUserDetails();
            }
            else if ("Password".Equals(Request.Form["hidSaveData"]))
            {
                UpdateUserPasswordData();
            }

            showData();
        }
        
        protected void UpdateUserDetails()
        {
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.UpdateUserPersonalInfo);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@full_name", inputName.Text);
            cmd.Parameters.AddWithValue("@signature", Convert.ToString(Editor1.Content));
            cmd.CommandType = CommandType.StoredProcedure;
            dbConnect.ExecuteNonQuery(cmd);
            user.FirstName = inputName.Text;
            Session["user"] = user;
        }
        protected void UpdateUserPasswordData()
        {
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.UpdateUserPasswordInfo);
            cmd.Parameters.AddWithValue("@login_id", user.Email);
            cmd.Parameters.AddWithValue("@pswd", EncrypterDecrypter.Encrypt(Convert.ToString(inputPassword.Text)));
            cmd.CommandType = CommandType.StoredProcedure;
            dbConnect.ExecuteNonQuery(cmd);

            inputPassword.Text = string.Empty;
            inputConfirmPassword.Text = string.Empty;
        }
        
        protected void showData()
        {
            DataSet ds = new DataSet();
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.UserDetail);

            string name = string.Empty;
            string email = string.Empty;
            string signature = string.Empty;

            cmd.Parameters.AddWithValue("@strEmailId", user.Email);
            cmd.CommandType = CommandType.StoredProcedure;
            ds = dbConnect.ExecuteDataSet(cmd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                name = cmnFncs.getValueForColumn(ds, "full_name");
                email = cmnFncs.getValueForColumn(ds, "email");
                signature = cmnFncs.getValueForColumn(ds, "email_signature");                 
            }
            ltlName.Text = name;
            ltlName1.Text = name;
            inputName.Text = name;
            ltlEmail.Text = email;
            ltlSignature.Text = signature;
            Editor1.Content = signature;
        }

        /// <summary>
        /// Save the uploaded image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SaveImage(object sender, EventArgs e)
        {
            try
            {
                string fileName = imageUpload.FileName;
                uploadImage(imageUpload);
                
            }
            catch(Exception ex)
            {
                Log.Error("Error occured during uploading image for " + user.Email + " and the error is " + ex.ToString());
            }
        }
        /// <summary>
        /// Remove image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RemoveImage(object sender, EventArgs e)
        {
            try
            {
                string fileName = string.Empty;
                deleteImageFile();
                SaveImage(fileName);

            }
            catch (Exception ex)
            {
                Log.Error("Error occured during deletion of image for " + user.Email + " and the error is " + ex.ToString());
            }
        }

        /// <summary>
        /// delete the actual file from file server
        /// </summary>
        private void deleteImageFile()
        {
            string imageUrl = string.Empty;
            string imageName = string.Empty;
            DataSet ds = new DataSet();
            clsDbConnect dbConnect = new clsDbConnect();
            string name = string.Empty;
            clsCommonFunction objCmnFncs = new clsCommonFunction();
            SqlCommand cmd = new SqlCommand(SPList.UserDetail);
            cmd.Parameters.AddWithValue("@strEmailId", user.Email);
            cmd.CommandType = CommandType.StoredProcedure;
            ds = dbConnect.ExecuteDataSet(cmd);
            imageName = objCmnFncs.getValueForColumn(ds, "image_name");
            if (imageName != "")
            {
                imageUrl = objCmnFncs.GetFilePathToSave("ImageFolder");
                imageUrl = imageUrl + imageName;
                if(File.Exists(imageUrl))
                {
                    File.Delete(imageUrl);
                }

            }
           
            
        }
        /// <summary>
        /// upload the image in file server
        /// </summary>
        /// <param name="image"></param>
        private void uploadImage(FileUpload image)
        {
            deleteImageFile();
            string imageName = string.Empty;
            imageName = Guid.NewGuid().ToString() + image.FileName;
            clsCommonFunction objCmnFncs = new clsCommonFunction();
            string imageUrl = objCmnFncs.GetFilePathToSave("ImageFolder");
            imageUrl = imageUrl + imageName;
            image.SaveAs(imageUrl);
            SaveImage(imageName);
        }
        /// <summary>
        /// update the db with actual image file name
        /// </summary>
        /// <param name="fileName"></param>
        private void SaveImage(string fileName)
        {
            
            clsDbConnect dbConnect = new clsDbConnect();
            string name = string.Empty;
            SqlCommand cmd = new SqlCommand(SPList.UpdateImageName);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@image_name", fileName); 
            cmd.CommandType = CommandType.StoredProcedure;
            dbConnect.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// returns the url for the image
        /// </summary>
        /// <returns></returns>
        protected string GetImageURl()
        {
            clsCommonFunction objClsCommonFunction = new clsCommonFunction();
            return objClsCommonFunction.GetImageURL(user);
        }
    }
}