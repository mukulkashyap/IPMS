/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using System;
using System.Collections.Generic;

namespace IPMS.Helper
{
    public class MailModel
    {
        public EmailAddress From { get; set; }
        public List<EmailAddress> To { get; set; }
        public List<EmailAddress> Bcc { get; set; }
        public List<EmailAddress> Cc { get; set; }
        public String Subject { get; set; }
        public string MailBody { get; set; }

    }

    public class EmailAddress
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}