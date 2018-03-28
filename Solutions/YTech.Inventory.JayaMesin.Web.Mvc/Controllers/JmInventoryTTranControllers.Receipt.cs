using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using System.Web.Mvc;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;
using YTech.Inventory.JayaMesin.Web.Mvc.Controllers.ViewModels;
using System.Linq;
using YTech.ServiceTracker.JayaMesin.Domain;
using System;
using System.Web;
using YTech.ServiceTracker.JayaMesin.Enums;
using System.Text;

namespace YTech.Inventory.JayaMesin.Web.Mvc.Controllers
{
    [HandleError]
    [Authorize]
    public partial class JmInventoryTTranController : Controller
    {
        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ReceiptList(bool? isModal)
        {
            if (isModal.HasValue)
                if (isModal.HasValue)
                    return View("Index", "~/Views/Shared/_NoMenuLayout.cshtml");

            PopulateTransStatus();
            return View();
        }

        public ActionResult ReceiptList_Read(string TransType, [DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetReceipts(TransType).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReceiptList_Create([DataSourceRequest] DataSourceRequest request, TransReceiptViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                JmInventoryTTran entity = new JmInventoryTTran();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToJmInventoryTTran(vm, entity);

                entity.TransNo = GetNewPONo();
                entity.TransType = EnumTransType.PO.ToString();
                entity.TransStatus = vm.TransStatus;
                entity.TransInputBy = User.Identity.Name;
                entity.TransInputDate = DateTime.Now;

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToJmInventoryTTran(TransReceiptViewModel vm, JmInventoryTTran entity)
        {
            entity.SupplierId = string.IsNullOrEmpty(vm.SupplierId) ? null : _JmInventoryMSupplierTasks.One(vm.SupplierId);
            //entity.TransRefId = string.IsNullOrEmpty(vm.TransRefId) ? null : _JmInventoryTTranTasks.One(vm.TransRefId);

            entity.TransNo = vm.TransNo;
            entity.TransDate = vm.TransDate;
            //entity.TransStatus = vm.TransStatus;
            //entity.TransApprovedBy = vm.TransApprovedBy;
            //entity.TransApprovedDate = vm.TransApprovedDate;
            //entity.TransInputBy = vm.TransInputBy;
            //entity.TransInputDate = vm.TransInputDate;
            entity.InvoiceNo = vm.InvoiceNo;
            entity.TransDesc = vm.TransDesc;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReceiptList_Update([DataSourceRequest] DataSourceRequest request, TransReceiptViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.TransId);
                if (entity != null)
                {
                    ConvertToJmInventoryTTran(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReceiptList_Destroy([DataSourceRequest] DataSourceRequest request, TransReceiptViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.TransId);
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

        private IEnumerable<TransReceiptViewModel> GetReceipts(string TransType)
        {
            var entitys = this._tasks.GetListByTransType(TransType);

            return from entity in entitys
                   select new TransReceiptViewModel
        {
            SupplierId = entity.SupplierId != null ? entity.SupplierId.Id : string.Empty,
            SupplierName = entity.SupplierId != null ? entity.SupplierId.SupplierName : string.Empty,
            //TransRefId = entity.TransRefId != null ? entity.TransRefId.Id : string.Empty,
            //TransRefIdName = entity.TransRefId != null ? entity.TransRefId.TransNo : string.Empty,

            TransNo = entity.TransNo,
            TransDate = entity.TransDate,
            //TransType = entity.TransType,
            TransStatus = entity.TransStatus,
            TransApprovedBy = entity.TransApprovedBy,
            TransApprovedDate = entity.TransApprovedDate,
            TransInputBy = entity.TransInputBy,
            TransInputDate = entity.TransInputDate,
            InvoiceNo = entity.InvoiceNo,
            TransDesc = entity.TransDesc,
            TransId = entity.Id
        };

        }
    }
}
