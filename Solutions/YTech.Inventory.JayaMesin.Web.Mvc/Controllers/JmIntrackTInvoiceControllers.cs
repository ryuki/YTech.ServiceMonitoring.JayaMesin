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
    public partial class JmIntrackTInvoiceController : Controller
    {
        private readonly IJmIntrackTInvoiceTasks _tasks;
        private readonly IJmIntrackTInvoiceLogTasks _IJmIntrackTInvoiceLogTasks;
        private readonly IJmInventoryTLogTasks _JmInventoryTLogTasks;
        public JmIntrackTInvoiceController(IJmIntrackTInvoiceTasks tasks, IJmIntrackTInvoiceLogTasks _IJmIntrackTInvoiceLogTasks, IJmInventoryTLogTasks _JmInventoryTLogTasks)
        {
            this._tasks = tasks;
            this._IJmIntrackTInvoiceLogTasks = _IJmIntrackTInvoiceLogTasks;
            this._JmInventoryTLogTasks = _JmInventoryTLogTasks;
        }

        [Authorize(Roles = "ADMINISTRATOR, MANAGER, SALES")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index(bool? isModal)
        {
            if (isModal.HasValue)
                if (isModal.HasValue)
                    return View("Index", "~/Views/Shared/_NoMenuLayout.cshtml");

            ///save log
            SaveLog(string.Empty, EnumLogType.Invoice, EnumTransLog.Open);

            PopulateInvoiceStatus();
            PopulateDocStatus();
            return View();
        }

        private void PopulateDocStatus()
        {
            var DocStatus = from EnumDocStatus e in Enum.GetValues(typeof(EnumDocStatus))
                            select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["doc_status"] = DocStatus;
        }

        private void PopulateInvoiceStatus()
        {
            var InvoiceStatus = from EnumInvoiceStatus e in Enum.GetValues(typeof(EnumInvoiceStatus))
                                select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["invoice_status"] = InvoiceStatus;
        }

        public ActionResult JmIntrackTInvoices_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetJmIntrackTInvoices().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmIntrackTInvoices_Create([DataSourceRequest] DataSourceRequest request, JmIntrackTInvoiceViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                JmIntrackTInvoice entity = new JmIntrackTInvoice();
                string invoiceid = Guid.NewGuid().ToString();
                entity.SetAssignedIdTo(invoiceid);

                ConvertToJmIntrackTInvoice(vm, entity);

                //modified date to tracking last update
                entity.ModifiedDate = DateTime.Now;
                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);

                ///save new invoice log
                ///
                SaveNewInvoiceLog(invoiceid, entity);
                SaveLog(invoiceid, EnumLogType.Invoice, EnumTransLog.Save);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void SaveNewInvoiceLog(string invoiceid, JmIntrackTInvoice inv)
        {
            JmIntrackTInvoiceLog log = new JmIntrackTInvoiceLog();
            log.SetAssignedIdTo(invoiceid);
            log.InvoiceAge = inv.InvoiceAge;
            log.InvoiceAmmount = inv.InvoiceAmmount;
            log.InvoiceDate = inv.InvoiceDate;
            log.InvoiceDelivery = inv.InvoiceDelivery;
            log.InvoiceDesc = inv.InvoiceDesc;
            log.InvoiceDocLetter = inv.InvoiceDocLetter;
            log.InvoiceDocSspPph = inv.InvoiceDocSspPph;
            log.InvoiceDocSspPpn = inv.InvoiceDocSspPpn;
            log.InvoiceDueDate = inv.InvoiceDueDate;
            log.InvoiceNo = inv.InvoiceNo;
            log.InvoicePacketAdmin = inv.InvoicePacketAdmin;
            log.InvoicePacketNo = inv.InvoicePacketNo;
            log.InvoicePaid = inv.InvoicePaid;
            log.InvoicePaidOffDate = inv.InvoicePaidOffDate;
            log.InvoiceStatus = inv.InvoiceStatus;
            log.ModifiedBy = inv.ModifiedBy;
            log.ModifiedDate = inv.ModifiedDate;
            log.SalesmanId = inv.SalesmanId;
            log.CustomerId = inv.CustomerId;
            log.DataStatus = inv.DataStatus;
            log.CreatedBy = inv.CreatedBy;
            log.CreatedDate = inv.CreatedDate;
            log.InvoiceHelpdesk = inv.InvoiceHelpdesk;
            log.InvoiceFundSource = inv.InvoiceFundSource;
            _IJmIntrackTInvoiceLogTasks.Insert(log);
        }

        private void SaveLog(string refid, EnumLogType logType, EnumTransLog transLog)
        {
            JmInventoryTLog woLog = new JmInventoryTLog();
            woLog.SetAssignedIdTo(Guid.NewGuid().ToString());
            woLog.LogRefId = refid;
            woLog.LogType = logType.ToString();
            woLog.LogUser = User.Identity.Name;
            woLog.LogDate = DateTime.Now;
            woLog.LogStatus = transLog.ToString();

            woLog.CreatedDate = DateTime.Now;
            woLog.CreatedBy = User.Identity.Name;
            woLog.DataStatus = EnumDataStatus.New.ToString();
            _JmInventoryTLogTasks.Insert(woLog);
        }

        private void ConvertToJmIntrackTInvoice(JmIntrackTInvoiceViewModel vm, JmIntrackTInvoice entity)
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
            entity.InvoiceHelpdesk = vm.InvoiceHelpdesk;
            entity.InvoiceFundSource = vm.InvoiceFundSource;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmIntrackTInvoices_Update([DataSourceRequest] DataSourceRequest request, JmIntrackTInvoiceViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.InvoiceId);
                if (entity != null)
                {
                    ConvertToJmIntrackTInvoice(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);

                    ///save new invoice log
                    ///
                    SaveNewInvoiceLog(Guid.NewGuid().ToString(), entity);
                    SaveLog(vm.InvoiceId, EnumLogType.Invoice, EnumTransLog.Update);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmIntrackTInvoices_Destroy([DataSourceRequest] DataSourceRequest request, JmIntrackTInvoiceViewModel vm)
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

                    ///save log
                    SaveLog(vm.InvoiceId, EnumLogType.Invoice, EnumTransLog.Delete);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<JmIntrackTInvoiceViewModel> GetJmIntrackTInvoices()
        {
            var entitys = this._tasks.GetListNotDeleted();

            string copyFormat = "No Paket : {0} \nTgl Faktur : {1:dd-MMM} \nNama Pelanggan : {2} \nNilai Faktur : {3:N0} \nAdmin : {4} \nSalesman : {5} \nHelpdesk : {6} \nStatus : {7}";

            return from entity in entitys
                   orderby entity.InvoiceDate descending, entity.InvoiceNo
                   select new JmIntrackTInvoiceViewModel
        {

            InvoiceNo = entity.InvoiceNo,
            InvoicePacketNo = entity.InvoicePacketNo,
            InvoiceDate = entity.InvoiceDate,
            CustomerId = entity.CustomerId,
            InvoiceAmmount = entity.InvoiceAmmount,
            InvoicePaid = entity.InvoicePaid,
            InvoicePacketAdmin = entity.InvoicePacketAdmin,
            SalesmanId = entity.SalesmanId,
            InvoiceAge = entity.InvoiceDate.HasValue ? (DateTime.Today - entity.InvoiceDate.Value).Days : 0,
            InvoiceDueDate = entity.InvoiceDueDate,
            InvoiceDelivery = entity.InvoiceDelivery,
            InvoicePaidOffDate = entity.InvoicePaidOffDate,
            InvoiceStatus = entity.InvoiceStatus,
            InvoiceDesc = string.Format("<{0:dd-MMM} by {1}> : {2}{2}{3}", DateTime.Today, User.Identity.Name, Environment.NewLine, entity.InvoiceDesc),
            InvoiceDocLetter = entity.InvoiceDocLetter,
            InvoiceDocSspPpn = entity.InvoiceDocSspPpn,
            InvoiceDocSspPph = entity.InvoiceDocSspPph,
            InvoiceId = entity.Id,
            InvoiceHelpdesk = entity.InvoiceHelpdesk,
            InvoiceFundSource = entity.InvoiceFundSource,
            InvoiceIsUrgent = (entity.InvoiceStatus == EnumInvoiceStatus.Lunas_Lengkap.ToString() || entity.InvoiceStatus == EnumInvoiceStatus.Baru_Dilunasi.ToString() || entity.InvoiceStatus == EnumInvoiceStatus.Lunas_BelumLengkap.ToString()) ? "Normal" : (entity.ModifiedDate.HasValue ? ((DateTime.Now - entity.ModifiedDate.Value).TotalHours >= 48 ? "Urgent" : "Normal") : "Normal"),
            InvoiceToCopy = string.Format(copyFormat, entity.InvoicePacketNo, entity.InvoiceDate, entity.CustomerId, entity.InvoiceAmmount, entity.InvoicePacketAdmin, entity.SalesmanId, entity.InvoiceHelpdesk, entity.InvoiceStatus),
            //use stored procedure, looping to get read flag make wo list slow response
            HaveBeenRead = _JmInventoryTLogTasks.GetHaveBeenLogType(entity.Id, User.Identity.Name, EnumLogType.Invoice, EnumTransLog.Read)
        };

        }

        public JsonResult PopulateJmIntrackTInvoices()
        {
            var list = from ent in _tasks.GetListNotDeleted()
                       select new
                               {
                                   Id = ent.Id,
                                   InvoiceNo = ent.InvoiceNo
                               };
            ViewData["JmIntrackTInvoices"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        [Authorize(Roles = "ADMINISTRATOR, MANAGER, SALES")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Dashboard(bool? isModal)
        {
            if (isModal.HasValue)
                if (isModal.HasValue)
                    return View("Index", "~/Views/Shared/_NoMenuLayout.cshtml");

            ///save log
            SaveLog(string.Empty, EnumLogType.Invoice_Dashboard, EnumTransLog.Open);

            PopulateInvoiceStatus();
            PopulateDocStatus();
            return View();
        }

        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AutoUpdateInvoiceStatus()
        {
            _tasks.AutoUpdateInvoiceStatus();
            return Content("AutoUpdateInvoiceStatus Done");
        }

        public ActionResult LogInvoice_Open(string random, string InvoiceId)
        {
            string msg = string.Empty;
            bool success = false;
            try
            {
                //save log
                SaveLog(InvoiceId, EnumLogType.Invoice, EnumTransLog.Read);

                success = true;
                msg = "Log InvoiceId success";
            }
            catch (Exception ex)
            {
                success = false;
                msg = ex.GetBaseException().Message;
            }
            var e = new
            {
                Success = success,
                Message = msg
            };
            return Json(e, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetDashboardInvoiceByStatus()
        {
            IList<JmIntrackTInvoice> invList = _tasks.GetListNotDeleted().ToList();
            DashboardInvoiceViewModel viewModel = new DashboardInvoiceViewModel();

            decimal blmLunas = 0;
            decimal sudahLunas = 0;
            for (var i = 0; i < invList.Count(); i++)
            {
                JmIntrackTInvoice inv = invList[i];
                if (inv.InvoiceStatus == EnumInvoiceStatus.Baru.ToString() || inv.InvoiceStatus == EnumInvoiceStatus.Dokumen.ToString() || inv.InvoiceStatus == EnumInvoiceStatus.Invoicing.ToString())
                {
                    blmLunas++;
                }
                else
                {
                    sudahLunas++;
                }
            }

            List<DashboardInvoiceViewModel> model = new List<DashboardInvoiceViewModel>()
            {
                new DashboardInvoiceViewModel(){ InvoiceStatus = "Belum Lunas", InvoiceTotal = decimal.Floor(blmLunas / (blmLunas+sudahLunas) * 100), Color = "#FFA500" },
                new DashboardInvoiceViewModel(){ InvoiceStatus = "Sudah Lunas", InvoiceTotal = decimal.Floor(sudahLunas / (blmLunas+sudahLunas) * 100), Color = "#9de219" }
            };
            return Json(model);
        }

        [HttpPost]
        public ActionResult GetDashboardInvoiceLunas()
        {
            IList<JmIntrackTInvoice> invList = _tasks.GetListNotDeleted().ToList();
            DashboardInvoiceViewModel viewModel = new DashboardInvoiceViewModel();

            decimal lunasLengkap = 0;
            decimal lunasBelumLengkap = 0;
            for (var i = 0; i < invList.Count(); i++)
            {
                JmIntrackTInvoice inv = invList[i];
                if (inv.InvoiceStatus == EnumInvoiceStatus.Lunas_BelumLengkap.ToString() || inv.InvoiceStatus == EnumInvoiceStatus.Baru_Dilunasi.ToString())
                {
                    lunasBelumLengkap++;
                }
                else
                {
                    lunasLengkap++;
                }
            }

            List<DashboardInvoiceViewModel> model = new List<DashboardInvoiceViewModel>()
            {
                new DashboardInvoiceViewModel(){ InvoiceStatus = "Belum Lengkap", InvoiceTotal = decimal.Floor(lunasBelumLengkap / (lunasLengkap+lunasBelumLengkap) * 100), Color = "#B0E0E6" },
                new DashboardInvoiceViewModel(){ InvoiceStatus = "Lengkap", InvoiceTotal = decimal.Floor(lunasLengkap / (lunasLengkap+lunasBelumLengkap) * 100), Color = "#DDA0DD" }
            };
            return Json(model);
        }
    }
}
