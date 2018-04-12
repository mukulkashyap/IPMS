/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/


using System;
using System.Data;
using System.Data.SqlClient;


namespace IPMS.Helper
{
    public class clsPatchCreation
    {
        public string PatchNumber { get; set; }
        public string FolderName { get; set; }
        public string getPatchURL(string clientName, string dependecyMonth)
        {
            clsCommonFunction objCommonFncs = new clsCommonFunction();
            string patchURL = string.Empty;
            string downloadLink = string.Empty;
            string date = DateTime.Now.Day.ToString() +  DateTime.Now.ToString("MMM").ToUpper() +  DateTime.Now.Year.ToString();
            string clntName = clientName;
            downloadLink = objCommonFncs.getValueFromConfig("downloadLinkURL");
            try
            {
                switch (clientName.ToUpper())
                {
                    case "ALL":
                        patchURL = getPatchLinkForAllHosted();
                        clntName = "ALL_HOSTED";
                        break;
                    default:
                        patchURL = getPatchLinkFoOthers(clientName);
                        clntName = clientName.ToUpper();
                        break;

                }

                
            }

          
            catch(Exception e)
            {
                Log.Error("Error While Patch Creation" + e.ToString());
            }
            FolderName = clntName + "_" + date + "_" + patchURL + ".zip";
            patchURL = downloadLink + FolderName;

            return patchURL;
        }

        /// <summary>
        /// Retuens the patch Link For All Hosted 
        /// </summary>
        /// <param name="dependecyMonth"></param>
        /// <returns></returns>
        private string getPatchLinkForAllHosted()
        {
            string patchURL = string.Empty;            
            clsCommonFunction objcommonFunction = new clsCommonFunction();
            DataSet ds = new DataSet();
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.MaxPatchNoForAllHosted);           
            cmd.CommandType = CommandType.StoredProcedure;
            ds = dbConnect.ExecuteDataSet(cmd);
            patchURL = objcommonFunction.getValueForColumn(ds, "patch_no");
            if (string.IsNullOrWhiteSpace(patchURL))
            {
                patchURL = objcommonFunction.getValueFromConfig("allHostedPatchId");                
            }
            PatchNumber = IncrementedPatchUrl(patchURL);
            return PatchNumber;
        }

        /// <summary>
        /// Increment the patch Number 
        /// </summary>
        /// <param name="patchURL"></param>
        /// <returns></returns>
        private string IncrementedPatchUrl(string patchURL)
        {
            string patchURLNew = string.Empty;
            int major = 0;
            int minor = 0;
            int revision = 0;
            string[] patch = patchURL.Split('.');
            major = int.Parse(patch[0]);
            minor = int.Parse(patch[1]);
            revision = int.Parse(patch[2]);
            if(revision ==99 && minor == 99)
            {
                revision = 0;
                minor = 0;
                major = major + 1;

            }
            else if (revision == 99)
            {
                revision = 0;
                minor = minor + 1;

            }
            else
            {
                revision = revision + 1;
            }
            patchURL = Convert.ToString(major) + "." + Convert.ToString(minor) + "." + Convert.ToString(revision);
            return patchURL;
        }

        /// <summary>
        /// Returns The patch Link for Client
        /// </summary>
        /// <param name="dependecyMonth"></param>
        /// <returns></returns>
        private string getPatchLinkFoOthers(string client)
        {
            string patchURL = string.Empty;
            int patchNo;
            clsCommonFunction objcommonFunction = new clsCommonFunction();
            DataSet ds = new DataSet();
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.MaxPatchNoForOther);
            cmd.Parameters.AddWithValue("@strClientName", client);
            cmd.CommandType = CommandType.StoredProcedure;
            ds = dbConnect.ExecuteDataSet(cmd);
            patchURL = objcommonFunction.getValueForColumn(ds, "patch_no");
            if (string.IsNullOrWhiteSpace(patchURL))
            {
                patchURL = "1";

            }
            else
            {
                patchNo = int.Parse(patchURL);
                patchURL = Convert.ToString(patchNo + 1);
            }
            PatchNumber = patchURL;
            return "M" + patchURL;
        }
    }
}