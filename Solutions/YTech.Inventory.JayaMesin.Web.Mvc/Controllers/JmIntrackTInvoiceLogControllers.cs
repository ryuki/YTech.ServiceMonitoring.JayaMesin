using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Web;
using YTech.ServiceTracker.JayaMesin.Enums;
using YTech.ServiceTracker.JayaMesin.Domain;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;
using YTech.Inventory.JayaMesin.Web.Mvc.Controllers.ViewModels;

namespace YTech.Inventory.JayaMesin.Web.Mvc.Controllers
{
    [HandleError]
    [Authorize]
    public partial class JmIntrackTInvoiceLogController : Controller
    {
       private readonly IJmIntrackTInvoiceLogTasks _tasks;
        public JmIntrackTInvoiceLogController(IJmIntrackTInvoiceLogTasks tasks)
        {
            this._tasks = tasks;
        }

        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index(bool? isModal)
        {
            if (isModal.HasValue)
                if (isModal.HasValue)
                    return View("Index", "~/Views/Shared/_NoMenuLayout.cshtml");

            return View();
        }

        public ActionResult JmIntrackTInvoiceLogs_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetJmIntrackTInvoiceLogs().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmIntrackTInvoiceLogs_Create([DataSourceRequest] DataSourceRequest request, JmIntrackTInvoiceLogViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                JmIntrackTInvoiceLog entity = new JmIntrackTInvoiceLog();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToJmIntrackTInvoiceLog(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToJmIntrackTInvoiceLog(JmIntrackTInvoiceLogViewModel vm, JmIntrackTInvoiceLog entity)
        {
            
            entity.InvoiceNo = vm.InvoiceNo;
            entity.InvoicePacketNo = vm.InvoicePacketNo;
            entity.InvoiceDate = vm.InvoiceDate;
            entity.CustomerId = vm.CustomerId;
            entity.InvoiceAmmount = vm.InvoiceAmmount;
            entity.InvoicePaid = vm.InvoicePaid;
            entity.InvoicePacketAdmin = vm.InvoicePacketAdmin;
            entity.SalesmanId = vm.SalesmanId;
            entity.InvoiceAge = vm.InvoiceAge;
            entity.InvoiceDueDate = vm.InvoiceDueDate;
            entity.InvoiceDelivery = vm.InvoiceDelivery;
            entity.InvoicePaidOffDate = vm.InvoicePaidOffDate;
            entity.InvoiceStatus = vm.InvoiceStatus;
            entity.InvoiceDesc = vm.InvoiceDesc;
            entity.InvoiceDocLetter = vm.InvoiceDocLetter;
            entity.InvoiceDocSspPpn = vm.InvoiceDocSspPpn;
            entity.InvoiceDocSspPph = vm.InvoiceDocSspPph;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmIntrackTInvoiceLogs_Update([DataSourceRequest] DataSourceRequest request, JmIntrackTInvoiceLogViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.InvoiceId);
                if (entity != null)
                {
                    ConvertToJmIntrackTInvoiceLog(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmIntrackTInvoiceLogs_Destroy([DataSourceRequest] DataSourceRequest request, JmIntrackTInvoiceLogViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.InvoiceId);
                if (entity != null)
                {
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Deleted.ToString();
                    _tasks.Update(entity);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<JmIntrackTInvoiceLogViewModel> GetJmIntrackTInvoiceLogs()
        {
            var entitys = this._tasks.GetListNotDeleted();

            return from entity in entitys
                   select new JmIntrackTInvoiceLogViewModel
        {
            
            InvoiceNo = entity.InvoiceNo,
            InvoicePacketNo = entity.InvoicePacketNo,
            InvoiceDate = entity.InvoiceDate,
            CustomerId = entity.CustomerId,
            InvoiceAmmount = entity.InvoiceAmmount,
            InvoicePaid = entity.InvoicePaid,
            InvoicePacketAdmin = entity.InvoicePacketAdmin,
            SalesmanId = entity.SalesmanId,
            InvoiceAge = entity.InvoiceAge,
            InvoiceDueDate = entity.InvoiceDueDate,
            InvoiceDelivery = entity.InvoiceDelivery,
            InvoicePaidOffDate = entity.InvoicePaidOffDate,
            InvoiceStatus = entity.InvoiceStatus,
            InvoiceDesc = entity.InvoiceDesc,
            InvoiceDocLetter = entity.InvoiceDocLetter,
            InvoiceDocSspPpn = entity.InvoiceDocSspPpn,
            InvoiceDocSspPph = entity.InvoiceDocSspPph,
            InvoiceId = entity.Id
        };

        }
        
        public JsonResult PopulateJmIntrackTInvoiceLogs()
        {
            var list = from ent in _tasks.GetListNotDeleted()
                            select new
                                    {
                                        Id = ent.Id,
                                        InvoiceNo = ent.InvoiceNo
                                    };
            ViewData["JmIntrackTInvoiceLogs"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
