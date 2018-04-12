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
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IPMS.Home
{
    public partial class CreatePatch : Base
    {
        User user = new User();
        clsPatchCreation objPatch = new clsPatchCreation();
        clsCommonFunction objCommon = new clsCommonFunction();
        string client = string.Empty;
        string codeBase = string.Empty;
        string patchURL = string.Empty;
        string patchName = Convert.ToString(Guid.NewGuid());
        static string SourceFilePath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            SecurityCheck();
            user = (User)Session["user"];
            if (user.UserType.ToUpper() != "W")
            {
                Response.Redirect("../Login/Logout");
            }
            lblFile.Text = "Upload your zip patch file in " + objCommon.GetKeyValuePairValue("patchUploadPath");
            if (!IsPostBack)
            {
                PopulateClientCombo();
                PopulateDependencyCombo();
            }


        }

        [System.Web.Services.WebMethod]
        public static string CheckUploadFile()
        {
            clsCommonFunction objCommon = new clsCommonFunction();
            string patchPath = objCommon.GetKeyValuePairValue("patchUploadPath");
            DirectoryInfo myDir = new DirectoryInfo(patchPath);
            String fileName = string.Empty;
            // Checking file is present or not 
            if(!myDir.Exists)
            {
                return patchPath + " is not present please inform Admin";
            }
            if (myDir.GetFiles().Length == 0)
            {
                return "There are no file present in the folder " + patchPath;
            }
            //Checking File Count 
            if (myDir.GetFiles().Length > 1)
            {
                return "More than 1 file is present in the folder " + patchPath;
            }
            // checking only zip file should be present
            foreach (var item in myDir.GetFiles())
            {
                fileName = item.Name;
                if (item.Extension.ToUpper() != ".ZIP")
                {
                    return "No ZIP file is present in the folder " + patchPath;
                }

            }
            SourceFilePath = patchPath + "\\" + fileName;
            return fileName;
        }

        /// <summary>
        /// On Click of Create Patch This event is called.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void SubmitMe(Object sender, EventArgs e)
        {
            patchURL = objPatch.getPatchURL(DropDownClient.SelectedValue, DropDownDependency.SelectedValue);
            hidPatchFolderName.Value = objPatch.FolderName;
            patchID.Text = patchURL;
            patchID.Visible = true;
            email.Visible = true;
            PopulateFrom();
            PopulateTo(DropDownClient.SelectedValue);
            PopulateCC(DropDownClient.SelectedValue);
            var ddList = DropDownDependency.Items.FindByValue(DropDownDependency.SelectedValue.ToString());
            switch (DropDownClient.SelectedValue.ToUpper())
            {
                case "ALL":
                    client = "All Hosted";
                    break;
                default:
                    client = DropDownClient.SelectedValue.ToString().ToUpper();
                    break;

            }
            codeBase = ddList.Text;
            hidClient.Value = DropDownClient.SelectedValue;
            hidCodeMonth.Value = codeBase;
            hidPatchNumber.Value = objPatch.PatchNumber;
            hidPatchUrl.Value = patchURL;
            txtSubject.Text = txtComments.Text;
            PopulateMessage(client, codeBase, patchURL, txtComments.Text);



        }
        /// <summary>
        /// On click of send email 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SendEmail(Object sender, EventArgs e)
        {
            clsCommonFunction obj = new clsCommonFunction();
            Task.Run(() => UploadPatch());            
            SendMail();
            InsertPatchDetail();
            obj.InsertActivity(user.Email, " has <span class='label label-success'>uploaded</span> a <a href='" + patchID.Text + "' download> Patch </a> for <span class='label label-primary'>" + DropDownClient.SelectedValue.ToUpper() + "</span>", "P");
            Response.Redirect("../PatchList/PatchList");
        }

        /// <summary>
        /// Send Email
        /// </summary>
        private void SendMail()
        {
            MailModel mailmodel = new MailModel();
            clsCommonFunction objCommonFncs = new clsCommonFunction();
            clsEmailFncs objEmailFncs = new clsEmailFncs();
            

            try
            {
                char[] delimiter = new char[] { ';' };
                string mailSubject = string.Empty;
                string mailBody = string.Empty;

                mailmodel.From = new EmailAddress
                {
                    Address = user.Email,
                    Name = user.FirstName + " " + user.LastName
                };

                objCommonFncs.getEmailAttendees(Convert.ToString(txtTo.Text), Convert.ToString(txtcc.Text), mailmodel);
                mailmodel.Subject = txtSubject.Text;
                mailBody = Convert.ToString(Editor1.Content);
                mailBody = mailBody.Replace("[[patchUrlLink]]", patchID.Text);
                mailmodel.MailBody = Convert.ToString(mailBody);

                objEmailFncs.SendEmailFncs(mailmodel);
            }
            catch (Exception ex)
            {
                Log.Error("Error while creating mail object!! Error Message: " + ex.Message);
            }
        }

        private void UploadPatch()
        {
            string patchName = patchID.Text.Substring(patchID.Text.LastIndexOf('/') + 1);
            clsFTPFileUpload ftpFileUpload = new clsFTPFileUpload();
            ftpFileUpload.UploadFIleToFTP(SourceFilePath,patchName, user);


        }

        /// <summary>
        /// Insert the patch detail in the Database
        /// </summary>
        private void InsertPatchDetail()
        {

            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.InsertPatchDetail);
            clsCommonFunction objCmn = new clsCommonFunction();
            cmd.Parameters.AddWithValue("@patch_month", hidCodeMonth.Value);
            cmd.Parameters.AddWithValue("@patch_comment", txtComments.Text);
            cmd.Parameters.AddWithValue("@patch_dependency", DropDownDependency.SelectedValue);
            cmd.Parameters.AddWithValue("@patch_url", patchID.Text);
            cmd.Parameters.AddWithValue("@client", hidClient.Value);
            cmd.Parameters.AddWithValue("@patch_no", hidPatchNumber.Value);
            cmd.Parameters.AddWithValue("@patch_uploaded_by", user.Email);
            cmd.CommandType = CommandType.StoredProcedure;
            dbConnect.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// To Populate From Email ID
        /// </summary>
        private void PopulateFrom()
        {
            from.Text = user.Email;
        }

        /// <summary>
        /// To Populate To Email Id
        /// </summary>
        /// <param name="clientName"></param>
        private void PopulateTo(string clientName)
        {
            txtTo.Text = GetKeyValue("HostedTo");
        }

        /// <summary>
        /// To Populate CC Email ID
        /// </summary>
        /// <param name="clientName"></param>
        private void PopulateCC(string clientName)
        {
            StringBuilder cc = new StringBuilder();
            cc.Append(GetKeyValue("HostedCC"));
            cc.Append(";");
            cc.Append(GetKeyValue("HostedCTS"));
            cc.Append(";");
            cc.Append(GetKeyValue("HostedEY"));
            cc.Append(";");
            cc.Append(GetKeyValue("HostedFragomen"));
            cc.Append(";");
            cc.Append(GetKeyValue("HostedIBM"));
            cc.Append(";");
            cc.Append(GetKeyValue("HostedLittler"));
            cc.Append(";");
            cc.Append(GetKeyValue("HostedMicrosoft"));
            cc.Append(";");
            cc.Append(GetKeyValue("HostedOgletree"));
            txtcc.Text = cc.ToString();
        }
        /// <summary>
        /// To Populate Email
        /// </summary>
        /// <param name="clientName"></param>
        private void PopulateMessage(string clientName, string dependencyName, string patchUrl, string taskName)
        {
            DataSet ds = new DataSet();
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.UserDetail);
            clsCommonFunction objCmn = new clsCommonFunction();
            cmd.Parameters.AddWithValue("@strEmailId", user.Email);
            cmd.CommandType = CommandType.StoredProcedure;
            ds = dbConnect.ExecuteDataSet(cmd);
            string message = objCmn.getValueForColumn(ds, "email_signature");
            message = message.Replace("[[dependency]]", dependencyName);
            message = message.Replace("[[clientName]]", clientName);
            message = message.Replace("[[patchUrl]]", patchURL);
            message = message.Replace("[[taskName]]", taskName);
            message = message.Replace("[[name]]", user.FirstName);

            Editor1.Content = message;
            Editor1.TopToolbarPreservePlace = false;
        }

        /// <summary>
        /// To Populate the client combo
        /// </summary>
        private void PopulateClientCombo()
        {
            DataTable dt = new DataTable();
            clsCommonFunction objCmnFnc = new clsCommonFunction();
            dt = objCmnFnc.GetKeyValuePairDetail("Client");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DropDownClient.DataSource = dt;
                    DropDownClient.DataValueField = "grp_val";
                    DropDownClient.DataTextField = "grp_key";
                    DropDownClient.DataBind();
                }
            }
            DropDownClient.Items.Insert(0, new ListItem("Select Client", ""));
            DropDownClient.Items.Insert(1, new ListItem("All Hosted", "all"));
        }

        /// <summary>
        /// To Populate Dependecy Combo
        /// </summary>
        private void PopulateDependencyCombo()
        {
            DataTable dt = new DataTable();
            clsCommonFunction objCmnFnc = new clsCommonFunction();
            dt = objCmnFnc.GetKeyValuePairDetail("Months");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DropDownDependency.DataSource = dt;
                    DropDownDependency.DataValueField = "grp_val";
                    DropDownDependency.DataTextField = "grp_key";
                    DropDownDependency.DataBind();
                }
            }
            DropDownDependency.Items.Insert(0, new ListItem("Select Dependency", ""));
            DropDownDependency.Items.Insert(1, new ListItem("No", "no"));
        }

        /// <summary>
        /// To Get Value from key Table
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetKeyValue(string key)
        {
            string value = string.Empty;
            DataTable dt = new DataTable();
            clsCommonFunction objCmnFnc = new clsCommonFunction();
            dt = objCmnFnc.GetKeyValuePairDetail(key);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    value = Convert.ToString(dt.Rows[0]["grp_val"]);
                }
            }
            return value;
        }


    }
}