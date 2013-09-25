using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;
using YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers.ViewModels;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers
{
    [HandleError]
    public class WOController : Controller
    {
        private readonly IMCustomerTasks _customerTasks;
        private readonly ITWOTasks _woTasks;
        public WOController(IMCustomerTasks customerTasks, ITWOTasks woTasks)
        {
            this._customerTasks = customerTasks;
            this._woTasks = woTasks;
        }

        public ActionResult Index()
        {
            PopulateCustomers();
            PopulateWOStatus();
            return View();
        }

        private void PopulateWOStatus()
        {
            var wostatus = from EnumWOStatus e in Enum.GetValues(typeof(EnumWOStatus))
                           select new { ID = e, Name = e.ToString() };
            ViewData["wo_status"] = wostatus;

        }

        private void PopulateCustomers()
        {
            IEnumerable<CustomerViewModel> customers = from cust in _customerTasks.GetAllCustomers()
                                                       select new CustomerViewModel
                                                       {
                                                           CustomerID = cust.Id,
                                                           CustomerName = cust.CustomerName,
                                                           CustomerPhone = cust.CustomerPhone,
                                                           CustomerAddress = cust.CustomerAddress
                                                       };
            ViewData["customers"] = customers;
        }

        public ActionResult WO_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetWOs().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WO_Create([DataSourceRequest] DataSourceRequest request, WOViewModel WOVM)
        {
            if (WOVM != null && ModelState.IsValid)
            {
                TWO wo = new TWO();
                wo.SetAssignedIdTo(Guid.NewGuid().ToString());

                wo.WONo = WOVM.WONo;
                wo.CustomerId = GetCustomer(WOVM.Customer.CustomerID);
                wo.WODate = WOVM.WODate;
                wo.WOItemType = WOVM.WOItemType;
                wo.WOItemSn = WOVM.WOItemSN;
                wo.WOIsGuarantee = WOVM.WOIsGuarantee;
                wo.WOEquipments = WOVM.WOEquipments;
                wo.WOScStore = WOVM.WOScStore;
                wo.WOPriority = WOVM.WOPriority;
                wo.WOStartDate = WOVM.WOStartDate;
                wo.WOLastStatus = WOVM.WOLastStatus;
                wo.WOEstFinishDate = WOVM.WOEstFinishDate;
                wo.WOTotal = WOVM.WOTotal;
                wo.WODp = WOVM.WODp;
                wo.WOInvoiceNo = WOVM.WOInvoiceNo;
                wo.WOTakenDate = WOVM.WOTakenDate;
                wo.WOBrokenDesc = WOVM.WOBrokenDesc;
                wo.WODesc = WOVM.WODesc;

                wo.CreatedDate = DateTime.Now;
                wo.CreatedBy = User.Identity.Name;
                wo.DataStatus = "New";

                _woTasks.Insert(wo);
            }

            return Json(new[] { WOVM }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WO_Update([DataSourceRequest] DataSourceRequest request, WOViewModel WOVM)
        {
            if (WOVM != null && ModelState.IsValid)
            {
                var wo = _woTasks.One(WOVM.WOID);
                if (wo != null)
                {
                    wo.WONo = WOVM.WONo;
                    wo.CustomerId = GetCustomer(WOVM.Customer.CustomerID);
                    wo.WODate = WOVM.WODate;
                    wo.WOItemType = WOVM.WOItemType;
                    wo.WOItemSn = WOVM.WOItemSN;
                    wo.WOIsGuarantee = WOVM.WOIsGuarantee;
                    wo.WOEquipments = WOVM.WOEquipments;
                    wo.WOScStore = WOVM.WOScStore;
                    wo.WOPriority = WOVM.WOPriority;
                    wo.WOStartDate = WOVM.WOStartDate;
                    wo.WOLastStatus = WOVM.WOLastStatus;
                    wo.WOEstFinishDate = WOVM.WOEstFinishDate;
                    wo.WOTotal = WOVM.WOTotal;
                    wo.WODp = WOVM.WODp;
                    wo.WOInvoiceNo = WOVM.WOInvoiceNo;
                    wo.WOTakenDate = WOVM.WOTakenDate;
                    wo.WOBrokenDesc = WOVM.WOBrokenDesc;
                    wo.WODesc = WOVM.WODesc;

                    wo.ModifiedDate = DateTime.Now;
                    wo.ModifiedBy = User.Identity.Name;
                    wo.DataStatus = "Updated";

                    _woTasks.Update(wo);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WO_Destroy([DataSourceRequest] DataSourceRequest request, WOViewModel WOVM)
        {
            if (WOVM != null)
            {
                var target = _woTasks.One(WOVM.WOID);
                if (target != null)
                {
                    _woTasks.Delete(target);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        private MCustomer GetCustomer(string custId)
        {
            return _customerTasks.One(custId);
        }

        private IEnumerable<WOViewModel> GetWOs()
        {
            var wos = this._woTasks.GetAllWOs();

            return from wo in wos
                   select new WOViewModel
                   {
                       WOID = wo.Id,
                       Customer = ConvertToCustomerVM(wo.CustomerId),
                       CustomerName = wo.CustomerId.CustomerName,
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
                   };
        }

        private CustomerViewModel ConvertToCustomerVM(Domain.MCustomer mCustomer)
        {
            CustomerViewModel custVM = new CustomerViewModel();
            if (mCustomer == null)
            {
                custVM.CustomerID = string.Empty;
                custVM.CustomerName = "Select a value";
            }
            else
            {
                custVM.CustomerID = mCustomer.Id;
                custVM.CustomerName = mCustomer.CustomerName;
            }
            return custVM;
        }

    }

    public enum EnumWOStatus
    {
        Baru,
        Dikerjakan,
        Tunggu,
        Selesai,
        Batal,
        Closed
    }
}
