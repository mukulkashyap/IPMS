/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/


using System;
using System.IO;
using WinSCP;


namespace IPMS.Helper
{
    public class clsFTPFileUpload
    {
        private string FTPUserName { get; set; }
        private string FTPPassword { get; set; }
        private string FTPurl { get; set; }
        private string SourceFileCompletePath { get; set; }

        private string FTPThumbprint { get; set; }
       
        public clsFTPFileUpload()
        {
            clsCommonFunction objCommonFncs = new clsCommonFunction();
            FTPUserName = objCommonFncs.getValueFromConfig("FTPUserName");
            FTPPassword = objCommonFncs.getValueFromConfig("FTPPassword");
            FTPurl = objCommonFncs.getValueFromConfig("FTPUrl");
            FTPThumbprint = objCommonFncs.getValueFromConfig("ftpThumbPrint"); 

        }

        /// <summary>
        /// Upload the file to SFTP server using WinScp.
        /// </summary>
        /// <param name="patchURL"></param>
        /// <param name="fileUpload"></param>
        public void UploadFIleToFTP(string sourceFileFullName,string patchURL,User user)
        {
            string message = patchURL + " Uploaded Succesfully";
            string subject = patchURL + " Uploaded Succesfully";

            clsCommonFunction objCommonFncs = new clsCommonFunction();
            try
            {
                
                MoveFile(sourceFileFullName,patchURL);
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Sftp,
                    HostName = FTPurl, //hostname e.g. IP: 192.54.23.32, or mysftpsite.com
                    UserName = FTPUserName,
                    Password = FTPPassword,
                    PortNumber = 22,
                    SshHostKeyFingerprint = FTPThumbprint
                };

                using (Session session = new Session())
                {
                    session.SessionLogPath = objCommonFncs.getValueFromConfig("filePath") + "sftpTransfer.txt";
                    session.Open(sessionOptions); //Attempts to connect to your sFtp site
                                                  //Get Ftp File

                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary; //The Transfer Mode - 
                                                                        //<em style="font-size: 9pt;">Automatic, Binary, or Ascii  
                    transferOptions.FilePermissions = null; //Permissions applied to remote files; 
                                                            //null for default permissions.  Can set user, 
                                                            //Group, or other Read/Write/Execute permissions. 
                    transferOptions.PreserveTimestamp = false; //Set last write time of 
                                                               //destination file to that of source file - basically change the timestamp 
                                                               //to match destination and source files.   
                    transferOptions.ResumeSupport.State = TransferResumeSupportState.Off;


                    TransferOperationResult transferResult;
                    //if (!session.FileExists("/test/"))
                    //{
                    //    session.CreateDirectory("/test/");
                    //}
                    //the parameter list is: local Path, Remote Path, Delete source file?, transfer Options  
                    transferResult = session.PutFiles(SourceFileCompletePath, patchURL, false, transferOptions);
                    //Throw on any error 
                    transferResult.Check();
                    //Log information and break out if necessary  
                }
            }
            catch (Exception ex)
            {
                message = patchURL + "Patch Upload Failed. Plaese upload manually. The Error is below" + ex.ToString();
                subject = patchURL + "Upload Failed";
            }
            finally
            { 
            SendMail(user, message, subject);
            }
        }

        /// <summary>
        /// Send the email about status of patch upload
        /// </summary>
        /// <param name="message"></param>
        /// <param name="subject"></param>
        private void SendMail(User user, string message,string subject)
        {
            clsCommonFunction objCommn = new clsCommonFunction();
            clsEmailFncs objEmailFncs = new clsEmailFncs();
            MailModel mailmodel = new MailModel();
            try
            {
                mailmodel.From = new EmailAddress
                {
                    Address = objCommn.getValueFromConfig("IPMSAdminEmail"),
                    Name = objCommn.getValueFromConfig("IPMSAdminDisplayName")
                };

                objCommn.getEmailAttendees(objCommn.getValueFromConfig("patchOwnerEmail") + ";" + user.Email, objCommn.getValueFromConfig("IPMSGroup"), mailmodel);
                mailmodel.Subject = subject;
                mailmodel.MailBody = message;

                objEmailFncs.SendEmailFncs(mailmodel);
            }
            catch (Exception ex)
            {
                Log.Error("Error while sending email!!! " + "Error Message: " + ex.ToString());
            }
        }

        /// <summary>
        /// Mpve the file from local to a temp folder so that we can find the actual location of file.
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <param name="PatchUrl"></param>
        private void MoveFile(string sourceFileFullName, string PatchUrl)
        {
            string filePath = string.Empty;
            clsCommonFunction objCommonFunction = new clsCommonFunction();
            filePath = objCommonFunction.getValueFromConfig("FTPFileFolder");            
            filePath = filePath + "\\" + PatchUrl;
            SourceFileCompletePath = filePath;
            File.Copy(sourceFileFullName, filePath, true);
                    
        }




    }


}