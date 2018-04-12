/**************************************************************************************************************************************************************************************
Name                                                                Date                                                                            Purpose
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Akshay Gaonkar                                                      June 15 2016                                                                    Created
Mukul kashyap                                                       August 05 2016                                                                  Modify to add all option in date report.
****************************************************************************************************************************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using IPMS.Helper;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using IPMS.Models;
using System.Data;

namespace IPMS.Controllers
{
    public class ReportController : BaseController
    {
        // GET: Report
        public ActionResult Index()
        {



            return View();
        }

        public ActionResult ClientWise()
        {
            List<SelectListItem> clients = new List<SelectListItem>();
            clients.Add(new SelectListItem { Value = "all", Text = "All Hosted" });
            clients.AddRange(new clsCommonFunction()
                             .GetKeyValuePairDetail("Client").Rows.OfType<DataRow>()
                             .Select(x =>
                             new SelectListItem
                             {
                                 Value = x.Field<string>("grp_val"),
                                 Text = x.Field<string>("grp_key"),
                             })
            );

            ViewBag.Clients = new SelectList(clients, "Value", "Text");

            List<SelectListItem> dependency = new List<SelectListItem>();
            dependency.Add(new SelectListItem { Value = "no", Text = "No" });
            dependency.AddRange(new clsCommonFunction()
                             .GetKeyValuePairDetail("Months").Rows.OfType<DataRow>()
                             .Select(x =>
                             new SelectListItem
                             {
                                 Value = x.Field<string>("grp_val"),
                                 Text = x.Field<string>("grp_key"),
                             })
            );

            ViewBag.Dependencies = new SelectList(dependency, "Value", "Text");
            return View();
        }

        public ActionResult DateWise()
        {
            List<SelectListItem> clients = new List<SelectListItem>();
            clients.Add(new SelectListItem { Value = "allclient", Text = "All" });
            clients.Add(new SelectListItem { Value = "all", Text = "All Hosted" });
            clients.AddRange(new clsCommonFunction()
                             .GetKeyValuePairDetail("Client").Rows.OfType<DataRow>()
                             .Select(x =>
                             new SelectListItem
                             {
                                 Value = x.Field<string>("grp_val"),
                                 Text = x.Field<string>("grp_key"),
                             })
            );

            ViewBag.Clients = new SelectList(clients, "Value", "Text");
            return View();
        }


        public ActionResult ClientWiseList([DataSourceRequest]DataSourceRequest request, string client, string patchDependency)
        {
            if (!String.IsNullOrWhiteSpace(client) && !String.IsNullOrWhiteSpace(patchDependency))
            {
                User user = new User();
                user = (User)Session["user"];
                clsCommonFunction objCommonFncs = new clsCommonFunction();
                string activity = "has <span class='label label-success'>pulled</span> a report for " + client.ToUpper() + " of the month <span class='label label-primary'>" + patchDependency.ToUpper() + "</span>";
                objCommonFncs.InsertActivity(user.Email, activity, "R");

                clsReport objClsReports = new clsReport();
                return Json(objClsReports.GetPatchDetails(client, patchDependency).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            return Json(new List<PatchDetails>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }
        public ActionResult DateWiseList([DataSourceRequest]DataSourceRequest request, string client, string fromDate, string toDate)
        {
           
            if (!String.IsNullOrWhiteSpace(client) && !String.IsNullOrWhiteSpace(fromDate) && !String.IsNullOrWhiteSpace(toDate))
            {
                User user = new User();
                user = (User)Session["user"];
                clsCommonFunction objCommonFncs = new clsCommonFunction();
                string activity = "has <span class='label label-success'>pulled</span> a report for " + client.ToUpper() + " from <span class='label label-warning'>" + fromDate + "</span> To <span class='label label-info'>" + toDate + "</span>";
                objCommonFncs.InsertActivity(user.Email, activity, "R");

                clsReport objClsReports = new clsReport();
                return Json(objClsReports.GetPatchDetails(client, fromDate, toDate).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            return Json(new List<PatchDetails>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }



        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    }
}