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
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult NewSales(string EditTransId)
        {
            TransPOViewModel vm = new TransPOViewModel();
            if (string.IsNullOrEmpty(EditTransId))
            {
                ///for first load, save first, to handle detail form input
                string newId = Guid.NewGuid().ToString();
                JmInventoryTTran entity = new JmInventoryTTran();
                entity.SetAssignedIdTo(newId);
                vm.TransId = newId;
                _tasks.Insert(entity);
            }
            else
            {
                JmInventoryTTran trans = this._tasks.One(EditTransId);

                vm.SupplierId = trans.SupplierId != null ? trans.SupplierId.Id : string.Empty;
                vm.SupplierName = trans.SupplierId != null ? trans.SupplierId.SupplierName : string.Empty;

                JmInventoryMSupplierViewModel supplierVm = new JmInventoryMSupplierViewModel();
                if (trans.SupplierId != null)
                {
                    supplierVm.SupplierId = trans.SupplierId.Id;
                    supplierVm.SupplierName = trans.SupplierId.SupplierName;
                }
                //vm.SupplierIdForEdit = supplierVm;

                vm.TransNo = trans.TransNo;
                vm.TransDate = trans.TransDate;
                //TransType = entity.TransType;
                vm.TransStatus = trans.TransStatus;
                vm.TransApprovedBy = trans.TransApprovedBy;
                vm.TransApprovedDate = trans.TransApprovedDate;
                vm.TransInputBy = trans.TransInputBy;
                vm.TransInputDate = trans.TransInputDate;
                vm.InvoiceNo = trans.InvoiceNo;
                vm.TransDesc = trans.TransDesc;
                vm.TransId = trans.Id;
            }
            return View(vm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewSales([DataSourceRequest] DataSourceRequest request, TransPOViewModel vm, string EditTransId)
        {
            if (vm != null && ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(EditTransId))
                {
                    var entity = _tasks.One(vm.TransId);

                    ConvertToJmInventoryTTran(vm, entity);

                    entity.TransType = EnumTransType.PO.ToString();
                    entity.TransStatus = EnumTransStatus.Baru.ToString();
                    entity.TransInputBy = User.Identity.Name;
                    entity.TransInputDate = DateTime.Now;

                    entity.CreatedDate = DateTime.Now;
                    entity.CreatedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.New.ToString();

                    _tasks.Update(entity);
                }
                else
                {
                    var entity = _tasks.One(EditTransId);
                    if (entity != null)
                    {
                        ConvertToJmInventoryTTran(vm, entity);

                        entity.ModifiedDate = DateTime.Now;
                        entity.ModifiedBy = User.Identity.Name;
                        entity.DataStatus = EnumDataStatus.Updated.ToString();

                        _tasks.Update(entity);
                    }
                }
            }

            return RedirectToAction("SalesList", "JmInventoryTTran", new { TransStatus = EnumTransStatus.Baru });
        }

        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SalesList(bool? isModal, EnumTransStatus TransStatus)
        {
            if (isModal.HasValue)
                if (isModal.HasValue)
                    return View("Index", "~/Views/Shared/_NoMenuLayout.cshtml");

            PopulateTransStatus();

            TransPOViewModel vm = new TransPOViewModel();
            if (TransStatus == EnumTransStatus.Baru)
            {
                vm.UserCanUpdateStatus = true;
                vm.UserCanEdit = true;
                vm.UserCanDelete = true;
                vm.UserCanView = false;
                vm.UserCanInputSN = false;
            }
            else if (TransStatus == EnumTransStatus.Disetujui)
            {
                vm.UserCanUpdateStatus = true;
                vm.UserCanEdit = false;
                vm.UserCanDelete = true;
                vm.UserCanView = false;
                vm.UserCanInputSN = false;
            }
            else if (TransStatus == EnumTransStatus.Ditolak)
            {
                vm.UserCanUpdateStatus = false;
                vm.UserCanEdit = false;
                vm.UserCanDelete = true;
                vm.UserCanView = true;
                vm.UserCanInputSN = false;
            }
            else if (TransStatus == EnumTransStatus.Diproses)
            {
                vm.UserCanUpdateStatus = false;
                vm.UserCanEdit = false;
                vm.UserCanDelete = true;
                vm.UserCanView = true;
                vm.UserCanInputSN = true;
            }
            return View(vm);
        }
    }
}
