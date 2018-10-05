using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;
using YTech.ServiceTracker.JayaMesin.Enums;
using YTech.Inventory.JayaMesin.Web.Mvc.Controllers.ViewModels;
using Microsoft.CSharp;

namespace YTech.Inventory.JayaMesin.Web.Mvc.Controllers
{
    [HandleError]
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly IMCustomerTasks _customerTasks;
        private readonly ITWOTasks _woTasks;
        private readonly IJmInventoryTProductPriceTasks _JmInventoryTProductPriceTasks;
        public ReportsController(IMCustomerTasks customerTasks, ITWOTasks woTasks,IJmInventoryTProductPriceTasks _JmInventoryTProductPriceTasks)
        {
            this._customerTasks = customerTasks;
            this._woTasks = woTasks;
            this._JmInventoryTProductPriceTasks = _JmInventoryTProductPriceTasks;
        }

        [Authorize(Roles = "ADMINISTRATOR, MANAGER, SALES")]
        public ActionResult Index(EnumInvReports rpt)
        {
            string title = string.Empty;
            switch (rpt)
            {
                case EnumInvReports.RptProductPriceBySupplier:
                    title = "Laporan Harga Produk Per Supplier";
                    break;
            }
            ViewBag.Title = title;

            ReportsViewModel rptVM = new ReportsViewModel();
            return View(rptVM);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(EnumInvReports rpt, ReportsViewModel rptVM)
        {
            ReportDataSource[] repCol = new ReportDataSource[1];
            ReportParameterCollection paramCol = null;
            switch (rpt)
            {
                case EnumInvReports.RptProductPriceBySupplier:
                    repCol[0] = GetProductPrices();

                    ////set params 
                    //paramCol = new ReportParameterCollection();
                    //paramCol.Add(new ReportParameter("V_START_DATE", rptVM.RptDateFrom.Value.ToString()));
                    //paramCol.Add(new ReportParameter("V_END_DATE", rptVM.RptDateTo.Value.ToString()));
                    break;
            }

            //send report data source and report params to session data
            Session["ReportData"] = repCol;
            Session["ReportParams"] = paramCol;

            var e = new
            {
                Success = true,
                Message = "redirect",
                UrlReport = string.Format("{0}", rpt.ToString())
            };
            return Json(e, JsonRequestBehavior.AllowGet);
        }

        private ReportDataSource GetProductPrices()
        {
            var entitys = this._JmInventoryTProductPriceTasks.GetListNotDeleted();

            var vm = from entity in entitys
                   select new JmInventoryTProductPriceViewModel
                   {
                       ProductId = entity.ProductId != null ? entity.ProductId.Id : string.Empty,
                       ProductIdName = entity.ProductId != null ? entity.ProductId.ProductName : string.Empty,
                       SupplierId = entity.SupplierId != null ? entity.SupplierId.Id : string.Empty,
                       SupplierIdName = entity.SupplierId != null ? entity.SupplierId.SupplierName : string.Empty,

                       ProductPrice = entity.ProductPrice,
                       ProductPriceStatus = entity.ProductPriceStatus,
                       ProductPriceDesc = entity.ProductPriceDesc,
                       ProductPriceId = entity.Id,
                       ProductPriceImgUrl = entity.ProductPriceImgUrl,
                       ProductPriceDate = entity.ProductPriceDate,
                       ProductPriceQty = entity.ProductPriceQty
                   };

            ReportDataSource reportDataSource = new ReportDataSource("DSProductPrice", vm);
            return reportDataSource;
        }

        private ReportDataSource GetWOs(DateTime? rptDateFrom, DateTime? rptDateTo)
        {
            return null;
            //var wos = this._woTasks.GetWOByDate(rptDateFrom, rptDateTo);

            //var vm = from wo in wos
            //         select new WOViewModel
            //         {
            //             WOID = wo.Id,
            //             CustomerName = wo.CustomerId.CustomerName,
            //             CustomerPhone = wo.CustomerId.CustomerPhone,
            //             CustomerAddress = wo.CustomerId.CustomerAddress,
            //             WODate = wo.WODate,
            //             WONo = wo.WONo,
            //             WOItemType = wo.WOItemType,
            //             WOItemSN = wo.WOItemSn,
            //             WOIsGuarantee = wo.WOIsGuarantee,
            //             WOEquipments = wo.WOEquipments,
            //             WOScStore = wo.WOScStore,
            //             WOPriority = wo.WOPriority,
            //             WOBrokenDesc = wo.WOBrokenDesc,
            //             WOLastStatus = wo.WOLastStatus,
            //             WOStartDate = wo.WOStartDate,
            //             WOEstFinishDate = wo.WOEstFinishDate,
            //             WOTotal = wo.WOTotal,
            //             WODp = wo.WODp,
            //             WOTakenDate = wo.WOTakenDate,
            //             WOInvoiceNo = wo.WOInvoiceNo,
            //             WOComplain = wo.WOComplain
            //         };

            //ReportDataSource reportDataSource = new ReportDataSource("WOViewModel", vm);
            //return reportDataSource;
        }

    }
}
