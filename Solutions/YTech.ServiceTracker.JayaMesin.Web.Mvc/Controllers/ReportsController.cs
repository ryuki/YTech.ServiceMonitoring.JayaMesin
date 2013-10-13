using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;
using YTech.ServiceTracker.JayaMesin.Enums;
using YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers.ViewModels;

namespace YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers
{
    [HandleError]
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly IMCustomerTasks _customerTasks;
        private readonly ITWOTasks _woTasks;
        public ReportsController(IMCustomerTasks customerTasks, ITWOTasks woTasks)
        {
            this._customerTasks = customerTasks;
            this._woTasks = woTasks;
        }

        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, KASIR, TEKNISI")] 
        public ActionResult Index(EnumReports rpt)
        {
            string title = string.Empty;
            switch (rpt)
            {
                case EnumReports.RptWODailyRecap:
                    title = "Laporan Harian Servis Printer";
                    break;
            }
            ViewData["Title"] = title;

            ReportsViewModel rptVM = new ReportsViewModel();
            return View(rptVM);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(EnumReports rpt, ReportsViewModel rptVM)
        {
            ReportDataSource[] repCol = new ReportDataSource[1];
            switch (rpt)
            {
                case EnumReports.RptWODailyRecap:
                    repCol[0] = GetWOs(rptVM.RptDateFrom, rptVM.RptDateTo);
                    break;
            }
            Session["ReportData"] = repCol;

            var e = new
            {
                Success = true,
                Message = "redirect",
                UrlReport = string.Format("{0}", rpt.ToString())
            };
            return Json(e, JsonRequestBehavior.AllowGet);
        }

        private ReportDataSource GetWOs(DateTime? rptDateFrom, DateTime? rptDateTo)
        {
            var wos = this._woTasks.GetWOByDate(rptDateFrom, rptDateTo);

            var vm = from wo in wos
                     select new WOViewModel
                     {
                         WOID = wo.Id,
                         CustomerName = wo.CustomerId.CustomerName,
                         CustomerPhone = wo.CustomerId.CustomerPhone,
                         CustomerAddress = wo.CustomerId.CustomerAddress,
                         WODate = wo.WODate,
                         WONo = wo.WONo,
                         WOItemType = wo.WOItemType,
                         WOItemSN = wo.WOItemSn,
                         WOIsGuarantee = wo.WOIsGuarantee,
                         WOEquipments = wo.WOEquipments,
                         WOScStore = wo.WOScStore,
                         WOPriority = wo.WOPriority,
                         WOBrokenDesc = wo.WOBrokenDesc,
                         WOLastStatus = wo.WOLastStatus,
                         WOStartDate = wo.WOStartDate,
                         WOTotal = wo.WOTotal,
                         WODp = wo.WODp,
                         WOTakenDate = wo.WOTakenDate,
                         WOInvoiceNo = wo.WOInvoiceNo,
                         WOComplain = wo.WOComplain
                     };

            ReportDataSource reportDataSource = new ReportDataSource("WOViewModel", vm);
            return reportDataSource;
        }

    }
}
