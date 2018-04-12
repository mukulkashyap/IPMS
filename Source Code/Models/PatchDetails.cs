/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/


using System;

namespace IPMS.Models
{
    public class PatchDetails
    {       
        public string Client { get; set; }
        public string PatchDependency { get; set; }
        public string PatchMonth { get; set; }
        public string PatchComment { get; set; }
        public string PatchURL { get; set; }       
        public DateTime PatchCreatedTS { get; set; }
}
}