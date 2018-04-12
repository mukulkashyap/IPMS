/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using IPMS.Models;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
namespace IPMS.Helper
{
    public class clsReport
    {
        /// <summary>
        /// Returns The list of Patc Details for a particular client and dependency
        /// </summary>
        /// <param name="i_client"></param>
        /// <param name="i_patchDependency"></param>
        /// <returns></returns>
        public List<PatchDetails> GetPatchDetails(string i_client,string i_patchDependency)
        {
            List<PatchDetails> patchDetails = new List<PatchDetails>();
            clsCommonFunction objcommonFunction = new clsCommonFunction();
            DataSet ds = new DataSet();
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.PatchDetailClient);
            cmd.Parameters.AddWithValue("@client", i_client);
            cmd.Parameters.AddWithValue("@patch_dependency", i_patchDependency);
            cmd.CommandType = CommandType.StoredProcedure;
            ds = dbConnect.ExecuteDataSet(cmd);

            if (ds !=null &&  ds.Tables!=null && ds.Tables.Count > 0 && ds.Tables[0]!=null && ds.Tables[0].Rows.Count > 0)
            {
                patchDetails = ds.Tables[0].Rows.OfType<DataRow>().Select(dr =>
                    new PatchDetails
                    {
                        Client = dr.Field<string>("Client"),
                        PatchMonth = dr.Field<string>("PatchMonth"),
                        PatchComment = dr.Field<string>("PatchComment"),
                        PatchURL = dr.Field<string>("PatchURL"),                       
                        PatchCreatedTS = dr.Field<DateTime>("created_ts"),
                    }).ToList();
            }
            return patchDetails;
        }

        /// <summary>
        /// Returns The list of Patc Details for a particular client and date range
        /// </summary>
        /// <param name="i_client"></param>
        /// <param name="i_fromDate"></param>
        /// <param name="i_toDate"></param>
        /// <returns></returns>
        public List<PatchDetails> GetPatchDetails(string i_client, string i_fromDate, string i_toDate)
        {
            List<PatchDetails> patchDetails = new List<PatchDetails>();
            DateTime fromDate;
            DateTime toDate;        
            if(DateTime.TryParse(i_fromDate, out fromDate) && DateTime.TryParse(i_toDate,out toDate))
            {
                
                clsCommonFunction objcommonFunction = new clsCommonFunction();
                DataSet ds = new DataSet();
                clsDbConnect dbConnect = new clsDbConnect();
                SqlCommand cmd = new SqlCommand(SPList.PatchDetailClientDateRange);
                cmd.Parameters.AddWithValue("@client", i_client);
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);
                cmd.CommandType = CommandType.StoredProcedure;
                ds = dbConnect.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    patchDetails = ds.Tables[0].Rows.OfType<DataRow>().Select(dr =>
                    new PatchDetails
                    {
                        Client = dr.Field<string>("Client"),
                        PatchMonth = dr.Field<string>("PatchMonth"),
                        PatchComment = dr.Field<string>("PatchComment"),
                        PatchURL = dr.Field<string>("PatchURL"),                        
                        PatchCreatedTS = dr.Field<DateTime>("created_ts"),
                    }).ToList();
                }
            }
            return patchDetails;
        }

        /// <summary>
        /// Function to return virtual path to show download the release doc.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetCompletePatchUrl(string value)
        {
            clsCommonFunction objCommonFunction = new clsCommonFunction();                                    
            return objCommonFunction.GetFilePathToShow("FileFolder")+value;
        }

        public List<PatchDetails> GetPatchList()
        {
            List<PatchDetails> patchDetails = new List<PatchDetails>();
            

                clsCommonFunction objcommonFunction = new clsCommonFunction();
                DataSet ds = new DataSet();
                clsDbConnect dbConnect = new clsDbConnect();
                SqlCommand cmd = new SqlCommand(SPList.PatchDetailList);                
                cmd.CommandType = CommandType.StoredProcedure;
                ds = dbConnect.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 &&ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                patchDetails = ds.Tables[0].Rows.OfType<DataRow>().Select(dr =>
                new PatchDetails
                {
                    Client = dr.Field<string>("Client"),
                    PatchMonth = dr.Field<string>("PatchMonth"),
                    PatchComment = dr.Field<string>("PatchComment"),
                    PatchURL = dr.Field<string>("PatchURL"),                   
                    PatchCreatedTS = dr.Field<DateTime>("created_ts"),
                    }).ToList();
                }
            
            return patchDetails;
        }
    }
}