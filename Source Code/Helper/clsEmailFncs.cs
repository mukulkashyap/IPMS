/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using System;
using System.Net;
using System.Net.Mail;

namespace IPMS.Helper
{
    public class clsEmailFncs
    {

        #region Global Variables
        private string SmtpHost;
        private int SmtpPort;
        private bool IsLocalSmtp;
        private string UserName;
        private string Password;
        private bool EnableSSL;
        private readonly clsCommonFunction objCmnFncs;
        #endregion


        #region Constructors
        /// <summary>
        /// Constructor to initialize the basic SMTP properties
        /// </summary>
        public clsEmailFncs()
        {
            objCmnFncs = new clsCommonFunction();
            SmtpHost = objCmnFncs.getValueFromConfig("SmtpHost");
            SmtpPort = Convert.ToInt32(objCmnFncs.getValueFromConfig("SmtpPort"));
            IsLocalSmtp = Convert.ToBoolean(objCmnFncs.getValueFromConfig("IsLocalSmtp"));
            UserName = objCmnFncs.getValueFromConfig("userName");
            Password = objCmnFncs.getValueFromConfig("password");
            EnableSSL = (SmtpPort == 587);
        }
        #endregion

        #region FormMailMessageObject
        /// <summary>
        /// Construct Net.Mail.MailMessage object and call SendMail function to send the mail using SMTP
        /// </summary>
        /// <param name="mailobject"></param>
        public void SendEmailFncs(MailModel mailobject)
        {
            MailMessage objMail = null;
            try
            {
                objMail = new MailMessage
                {
                    From = new MailAddress(mailobject.From.Address, mailobject.From.Name),
                    Subject = mailobject.Subject,
                    IsBodyHtml = true,
                    Body = mailobject.MailBody,
                    BodyEncoding = System.Text.Encoding.UTF8
                };

                if (mailobject.To != null && mailobject.To.Count > 0)
                {
                    foreach (var mailTo in mailobject.To)
                    {
                        objMail.To.Add(new MailAddress(mailTo.Address));
                    }
                }

                if (mailobject.Cc != null && mailobject.Cc.Count > 0)
                {
                    foreach (var mailCC in mailobject.Cc)
                    {
                        objMail.CC.Add(new MailAddress(mailCC.Address));
                    }
                }

                if (mailobject.Bcc != null && mailobject.Bcc.Count > 0)
                {
                    foreach (var mailBcc in mailobject.Bcc)
                    {
                        objMail.Bcc.Add(new MailAddress(mailBcc.Address));
                    }
                }

                SendMail(objMail);
                Log.Activity(
                    "Mail has been successfully sent!! Email From: " + Convert.ToString(objMail.From.Address) + "; Email Subject: " + Convert.ToString(objMail.Subject) + ";");

            }
            catch (Exception ex)
            {
                Log.Error(
                    "Error Sending Email in SendEmailFncs()!! Email From: " + Convert.ToString(objMail.From.Address) + "; Email Subject: " + Convert.ToString(objMail.Subject) + "; Error Message: " + ex.Message);
            }
        }
        #endregion

        #region SendMail
        /// <summary>
        /// Send Mail using SMTP client
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <returns></returns>
        private void SendMail(MailMessage mailMessage)
        {
            try
            {
                var smtpClient = new SmtpClient(SmtpHost, SmtpPort);

                if (!IsLocalSmtp)
                {
                    smtpClient.Credentials =
                        new NetworkCredential(UserName, Password);
                    smtpClient.EnableSsl = EnableSSL;
                }

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;                
                smtpClient.Send(mailMessage);
            }
            catch (SmtpException ex)
            {
                Log.Activity(
                    "Error Sending Email in SendMail()!! Email From: " + Convert.ToString(mailMessage.From.Address) + "; Email Subject: " + Convert.ToString(mailMessage.Subject) + "; Error Message: " + ex.Message);

            }
            catch (Exception ex)
            {
                Log.Activity(
                    "Error Sending Email in SendMail()!! Email From: " + Convert.ToString(mailMessage.From.Address) + "; Email Subject: " + Convert.ToString(mailMessage.Subject) + "; Error Message: " + ex.Message);

            }
        }

        #endregion
    }
}