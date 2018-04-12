/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using IPMS.Models;
using System.Data;
using System.Data.SqlClient;

namespace IPMS.Helper
{
    public class clsActivity
    {
        /// <summary>
        /// return the list of activity
        /// </summary>
        /// <returns></returns>
        public List<Activity> GetActivity()
        {
            List<Activity> activities = new List<Activity>();
            clsCommonFunction objcommonFunction = new clsCommonFunction();
            DataSet ds = new DataSet();
            clsDbConnect dbConnect = new clsDbConnect();
            SqlCommand cmd = new SqlCommand(SPList.ActivityList);
            cmd.CommandType = CommandType.StoredProcedure;
            ds = dbConnect.ExecuteDataSet(cmd);

            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                activities = ds.Tables[0].Rows.OfType<DataRow>().Select(dr =>
                    new Activity
                    {
                        ActivityType = dr.Field<string>("activity_type"),
                        ActivityDoneBy = dr.Field<string>("email"),
                        ActivityDescription = dr.Field<string>("activity"),
                        ActivityDate = dr.Field<DateTime>("created_ts"),
                    }).ToList();
            }
            return activities;
        }
    }
}