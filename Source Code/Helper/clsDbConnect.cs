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
    public class clsDbConnect
    {

        /// <summary>
        /// returns dataset after executing the query
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
       
        public DataSet ExecuteDataSet(SqlCommand cmd)
        {
            DataSet dt = new DataSet();
            string strSQLconstring = System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnection"].ToString();
            SqlConnection objConnection = new SqlConnection(strSQLconstring);
            try
            {
                objConnection.Open();
                cmd.Connection = objConnection;
                SqlDataAdapter sDataAdapter = new SqlDataAdapter(cmd);
                DataSet dSet = new DataSet();
                sDataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Log.Error("Error Occured While executing " + cmd.CommandText + " and the error is " + ex.ToString());
                HttpContext.Current.Response.Redirect("Error.aspx",false);
                 
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return dt;


        }

        /// <summary>
        /// execute the non query
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public void ExecuteNonQuery(SqlCommand cmd)
        {
            
            string strSQLconstring = System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnection"].ToString();
            SqlConnection objConnection = new SqlConnection(strSQLconstring);
            try
            {
                objConnection.Open();
                cmd.Connection = objConnection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.Error("Error Occured While executing " + cmd.CommandText + " and the error is " + ex.ToString());
                HttpContext.Current.Response.Redirect("Error.aspx",false);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            


        }
    }
}