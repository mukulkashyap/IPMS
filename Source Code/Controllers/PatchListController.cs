/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Mukul Kashyap                                                       June 15 2016                                                                    Created

****************************************************************************************************************************************************************************************/

using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using IPMS.Helper;

namespace IPMS.Controllers
{
    public class PatchListController : BaseController
    {
        // GET: PatchList
        public ActionResult PatchList()
        {
            return View();
        }

        public ActionResult GetPatchList([DataSourceRequest]DataSourceRequest request)
        {
            
                clsReport objClsReports = new clsReport();
                return Json(objClsReports.GetPatchList().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            
            
        }
    }
}