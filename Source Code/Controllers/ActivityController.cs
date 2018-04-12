
/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using IPMS.Helper;
using IPMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Controllers
{
    public class ActivityController : BaseController
    {
        // GET: Activity
        public ActionResult Activity()
        {
            clsActivity objClsActivity = new clsActivity();
            List<Activity> activities = objClsActivity.GetActivity();
            return View("Activity", activities);
        }
    }
}