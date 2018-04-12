/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using System;

namespace IPMS.Helper
{
    public static class clsCheckSecurity
    {
        public static bool securityCheck(User user)
        {
            return checkSessionExist(user);
        }

        /// <summary>
        /// If the sesion doesnt exist then returns fail
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static bool checkSessionExist(User user)
        {

            bool IsSessionExist = false;
            try
            {
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(user.Email))
                    {
                        IsSessionExist = true;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Message("1000", Environment.NewLine + "Logout From The Page - " + e.ToString());
            }

            return IsSessionExist;
        }

    }
}
