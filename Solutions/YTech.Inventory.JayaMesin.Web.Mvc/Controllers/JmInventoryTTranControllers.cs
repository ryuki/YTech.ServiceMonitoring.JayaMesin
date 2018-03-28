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
        private readonly IJmInventoryTTranTasks _tasks;
        private readonly IJmInventoryMSupplierTasks _JmInventoryMSupplierTasks;
        private readonly IJmInventoryTTranTasks _JmInventoryTTranTasks;
        private readonly IJmInventoryTLogTasks _JmInventoryTLogTasks;
        private readonly IJmInventoryTReferenceTasks _JmInventoryTReferenceTasks;
        private readonly IJmInventoryTTransDetTasks _IJmInventoryTTransDetTasks;

        public JmInventoryTTranController(IJmInventoryTTranTasks tasks, IJmInventoryMSupplierTasks _JmInventoryMSupplierTasks, IJmInventoryTTranTasks _JmInventoryTTranTasks, IJmInventoryTLogTasks _JmInventoryTLogTasks, IJmInventoryTReferenceTasks _JmInventoryTReferenceTasks, IJmInventoryTTransDetTasks _IJmInventoryTTransDetTasks)
        {
            this._tasks = tasks;
            this._JmInventoryMSupplierTasks = _JmInventoryMSupplierTasks;
            this._JmInventoryTTranTasks = _JmInventoryTTranTasks;
            this._JmInventoryTLogTasks = _JmInventoryTLogTasks;
            this._JmInventoryTReferenceTasks = _JmInventoryTReferenceTasks;
            this._IJmInventoryTTransDetTasks = _IJmInventoryTTransDetTasks;
        }

        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult POList(bool? isModal, EnumTransStatus TransStatus)
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

        public ActionResult POList_Read(string TransType, EnumTransStatus TransStatus, [DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetJmInventoryTTrans(TransType, TransStatus).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult POList_Create([DataSourceRequest] DataSourceRequest request, TransPOViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                JmInventoryTTran entity = new JmInventoryTTran();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToJmInventoryTTran(vm, entity);

                ///transaction no manual input
                //entity.TransNo = GetNewPONo();
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

        private void ConvertToJmInventoryTTran(TransPOViewModel vm, JmInventoryTTran entity)
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

            ///calculate total base on detail
            CalculateTotal(vm.TransId, entity);
        }

        private void CalculateTotal(string TransId, JmInventoryTTran entity)
        {
            var dets = this._IJmInventoryTTransDetTasks.GetListByTransId(TransId);
            entity.TransSubTotal = dets.Sum(p => p.TransDetTotal);
            entity.TransGrandTotal = entity.TransSubTotal;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult POList_Update([DataSourceRequest] DataSourceRequest request, TransPOViewModel vm)
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
        public ActionResult POList_Destroy([DataSourceRequest] DataSourceRequest request, TransPOViewModel vm)
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

        private IEnumerable<TransPOViewModel> GetJmInventoryTTrans(string TransType, EnumTransStatus TransStatus)
        {
            var entitys = this._tasks.GetListByTransTypeAndStatus(TransType, TransStatus.ToString());

            return from entity in entitys
                   select new TransPOViewModel
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

        public JsonResult PopulateJmInventoryTTrans()
        {
            var list = from ent in _tasks.GetListNotDeleted()
                       select new
                               {
                                   Id = ent.Id,
                                   TransIdName = ent.TransNo
                               };
            ViewData["JmInventoryTTrans"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        private void PopulateTransStatus()
        {
            var trans_status = from EnumTransStatus e in Enum.GetValues(typeof(EnumTransStatus))
                               select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["trans_status"] = trans_status;
        }

        public ActionResult POList_Approve(string random, string TransId, string TransStatus)
        {
            bool success = true;
            string debug = string.Empty;

            try
            {
                //get po by TransId
                JmInventoryTTran trans = this._tasks.One(TransId);

                if (trans != null)
                {
                    if (TransStatus == EnumTransStatus.Disetujui.ToString())
                    {
                        trans.TransStatus = EnumTransStatus.Disetujui.ToString();
                        trans.TransApprovedBy = User.Identity.Name;
                        trans.TransApprovedDate = DateTime.Now;
                    }
                    else if (TransStatus == EnumTransStatus.Ditolak.ToString())
                    {
                        trans.TransStatus = EnumTransStatus.Ditolak.ToString();
                        trans.TransApprovedBy = User.Identity.Name;
                        trans.TransApprovedDate = DateTime.Now;
                    }
                    else if (TransStatus == EnumTransStatus.Diproses.ToString())
                    {
                        trans.TransStatus = EnumTransStatus.Diproses.ToString();
                        trans.TransApprovedBy = User.Identity.Name;
                        trans.TransApprovedDate = DateTime.Now;
                    }

                    trans.ModifiedDate = DateTime.Now;
                    trans.ModifiedBy = User.Identity.Name;
                    trans.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(trans);

                    //save logx
                    SaveLog(trans, EnumLogType.Trans_PO, EnumTransLog.Approve);

                    success = true;
                    debug = "Berhasil";
                }
            }
            catch (Exception ex)
            {
                success = false;
                debug = ex.GetBaseException().Message;
            }


            var result = new
            {
                Success = success.ToString(),
                Message = debug
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private void SaveLog(JmInventoryTTran trans, EnumLogType logType, EnumTransLog transLog)
        {
            JmInventoryTLog woLog = new JmInventoryTLog();
            woLog.SetAssignedIdTo(Guid.NewGuid().ToString());
            woLog.LogRefId = trans.Id;
            woLog.LogType = logType.ToString();
            woLog.LogUser = User.Identity.Name;
            woLog.LogDate = DateTime.Now;
            woLog.LogStatus = transLog.ToString();

            woLog.CreatedDate = DateTime.Now;
            woLog.CreatedBy = User.Identity.Name;
            woLog.DataStatus = EnumDataStatus.New.ToString();
            _JmInventoryTLogTasks.Insert(woLog);
        }

        private string GetNewPONo()
        {
            JmInventoryTReference refer = _JmInventoryTReferenceTasks.GetByReferenceType(EnumInventoryReferenceType.PONo);
            bool automatedIncrease = true;
            decimal no = Convert.ToDecimal(refer.ReferenceValue) + 1;

            //reset wo no to 1 every month
            if (DateTime.Today.Day == 1 && refer.ModifiedDate < DateTime.Today)
                no = 1;

            refer.ReferenceValue = no.ToString();
            if (automatedIncrease)
            {
                refer.ModifiedDate = DateTime.Now;
                refer.ModifiedBy = User.Identity.Name;
                refer.DataStatus = EnumDataStatus.New.ToString();
                _JmInventoryTReferenceTasks.Update(refer);
            }

            StringBuilder result = new StringBuilder();
            result.Append("PO[YEAR][MONTH][XXX]");
            result.Replace("[XXX]", GetNo(3, no));
            result.Replace("[DAY]", DateTime.Today.Day.ToString());
            result.Replace("[MONTH]", DateTime.Today.ToString("MM").ToUpper());
            result.Replace("[YEAR]", DateTime.Today.ToString("yy"));
            return result.ToString();
        }

        private static string GetNo(int maxLength, decimal no)
        {
            int len = maxLength - no.ToString().Length;
            string factur = no.ToString();
            for (int i = 0; i < len; i++)
            {
                factur = "0" + factur;
            }
            return factur;
        }

        public ActionResult LogTrans_Open(string random, string TransId)
        {
            string msg = string.Empty;
            bool success = true;
            if (!string.IsNullOrEmpty(TransId))
            {
                try
                {
                    //get wo by PO id
                    JmInventoryTTran wo = this._JmInventoryTTranTasks.One(TransId);
                    //save log
                    SaveLog(wo, EnumLogType.Trans_PO, EnumTransLog.Read);

                    success = true;
                    msg = "Log PO success";

                }
                catch (Exception ex)
                {
                    success = false;
                    msg = ex.GetBaseException().Message;
                }
            }
            var e = new
            {
                Success = success,
                Message = msg
            };
            return Json(e, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult NewPO(string EditTransId)
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
        public ActionResult NewPO([DataSourceRequest] DataSourceRequest request, TransPOViewModel vm, string EditTransId)
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

            return RedirectToAction("POList", "JmInventoryTTran", new { TransStatus = EnumTransStatus.Baru });
        }


        public ActionResult ViewPOContent(string EditTransId)
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
            return PartialView(vm);
        }
    }
}
