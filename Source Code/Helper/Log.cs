/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using System;
using System.IO;
using System.Configuration;

namespace IPMS.Helper
{
    /// <summary>
    /// This class is used to log the text in text file.
    /// </summary>
    public static class Log
    {
        private static string DirectoryPath { get; set; }
        static Log()
        {

            DirectoryPath = ConfigurationManager.AppSettings["filePath"];
            
        }

        /// <summary>
        /// This function is used to log the message.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        public static void Message(string id,string message)
        {
            message = Environment.NewLine + message;
            string DirPath = DirectoryPath + "\\" + DateTime.Now.ToString("dd - MM - yyyy");
            if(!Directory.Exists(DirPath))
            {
                Directory.CreateDirectory(DirPath);
            }
            string fileName = DirPath + "\\" + id + ".txt";
            File.AppendAllText(fileName,   message + DateTime.Now.ToString());
        }

        /// <summary>
        /// This function will log the error. The error folder will be outside.
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            message = Environment.NewLine + message;
            string DirPath = DirectoryPath + "\\Error\\"  + DateTime.Now.ToString("dd - MM - yyyy");
            if (!Directory.Exists(DirPath))
            {
                Directory.CreateDirectory(DirPath);
            }
            string fileName = DirPath + "\\" + "error" + ".txt";
            File.AppendAllText(fileName,  message + DateTime.Now.ToString());
        }

        /// <summary>
        /// Log the activity
        /// </summary>
        /// <param name="message"></param>
        public static void Activity(string message)
        {
            message = Environment.NewLine + message;
            string DirPath = DirectoryPath + "\\Activity\\" + DateTime.Now.ToString("dd - MM - yyyy");
            if (!Directory.Exists(DirPath))
            {
                Directory.CreateDirectory(DirPath);
            }
            string fileName = DirPath + "\\" + "Activity" + ".txt";
            File.AppendAllText(fileName,  message + DateTime.Now.ToString());
        }

    }
}