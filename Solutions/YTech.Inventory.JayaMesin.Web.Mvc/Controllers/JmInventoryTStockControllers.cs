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

namespace YTech.Inventory.JayaMesin.Web.Mvc.Controllers
{
    [HandleError]
    [Authorize]
    public partial class JmInventoryTStockController : Controller
    {
        private readonly IJmInventoryTStockTasks _tasks;
        private readonly IJmInventoryMProductTasks _JmInventoryMProductTasks;
        private readonly IJmInventoryTTranTasks _IJmInventoryTTranTasks;
        private readonly IJmInventoryTTransDetTasks _IJmInventoryTTransDetTasks;

        public JmInventoryTStockController(IJmInventoryTStockTasks tasks, IJmInventoryMProductTasks _JmInventoryMProductTasks, IJmInventoryTTranTasks _IJmInventoryTTranTasks, IJmInventoryTTransDetTasks _IJmInventoryTTransDetTasks)
        {
            this._tasks = tasks;
            this._JmInventoryMProductTasks = _JmInventoryMProductTasks;
            this._IJmInventoryTTranTasks = _IJmInventoryTTranTasks;
            this._IJmInventoryTTransDetTasks = _IJmInventoryTTransDetTasks;
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

        public ActionResult JmInventoryTStocks_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetJmInventoryTStocks().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryTStocks_Create([DataSourceRequest] DataSourceRequest request, JmInventoryTStockViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                JmInventoryTStock entity = new JmInventoryTStock();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToJmInventoryTStock(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToJmInventoryTStock(JmInventoryTStockViewModel vm, JmInventoryTStock entity)
        {
            entity.ProductId = string.IsNullOrEmpty(vm.ProductId) ? null : _JmInventoryMProductTasks.One(vm.ProductId);

            entity.StockSn = vm.StockSn;
            entity.StockInTransId = vm.StockInTransId;
            entity.StockInDate = vm.StockInDate;
            entity.StockOutTransId = vm.StockOutTransId;
            entity.StockOutDate = vm.StockOutDate;
            entity.StockStatus = vm.StockStatus;
            entity.StockLocation = vm.StockLocation;
            entity.StockDesc = vm.StockDesc;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryTStocks_Update([DataSourceRequest] DataSourceRequest request, JmInventoryTStockViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.StockId);
                if (entity != null)
                {
                    ConvertToJmInventoryTStock(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryTStocks_Destroy([DataSourceRequest] DataSourceRequest request, JmInventoryTStockViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.StockId);
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

        private IEnumerable<JmInventoryTStockViewModel> GetJmInventoryTStocks()
        {
            var entitys = this._tasks.GetListNotDeleted();

            return from entity in entitys
                   select new JmInventoryTStockViewModel
        {
            ProductId = entity.ProductId != null ? entity.ProductId.Id : string.Empty,
            ProductIdName = entity.ProductId != null ? entity.ProductId.ProductName : string.Empty,

            StockSn = entity.StockSn,
            StockInTransId = entity.StockInTransId,
            StockInDate = entity.StockInDate,
            StockOutTransId = entity.StockOutTransId,
            StockOutDate = entity.StockOutDate,
            StockStatus = entity.StockStatus,
            StockLocation = entity.StockLocation,
            StockDesc = entity.StockDesc,
            StockId = entity.Id
        };

        }

        public JsonResult PopulateJmInventoryTStocks()
        {
            var list = from ent in _tasks.GetListNotDeleted()
                       select new
                               {
                                   Id = ent.Id,
                                   StockIdName = ent.StockLocation
                               };
            ViewData["JmInventoryTStocks"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult InputSN(string EditTransId)
        {
            TStockInputViewModel vm = new TStockInputViewModel();
            if (string.IsNullOrEmpty(EditTransId))
            {
                ///for first load, save first, to handle detail form input
                string newId = Guid.NewGuid().ToString();
                JmInventoryTTran entity = new JmInventoryTTran();
                entity.SetAssignedIdTo(newId);
                vm.TransId = newId;
                vm.Stocks = new List<JmInventoryTStockViewModel>();
                _IJmInventoryTTranTasks.Insert(entity);
            }
            else
            {
                JmInventoryTTran trans = this._IJmInventoryTTranTasks.One(EditTransId);

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
                vm.Stocks = new List<JmInventoryTStockViewModel>();
            }
            return View(vm);
        }

        public ActionResult ViewSNContent(string EditTransDetId)
        {
            TStockInputViewModel vm = new TStockInputViewModel();
            vm.TransDetId = EditTransDetId;

            JmInventoryTTransDet det = _IJmInventoryTTransDetTasks.One(EditTransDetId);
            List<JmInventoryTStockViewModel> liststockvm = new List<JmInventoryTStockViewModel>();
            if (det != null)
            {
                IEnumerable<JmInventoryTStock> stocks = this._tasks.GetListByTransDetId(det.Id);
                string productname = det.ProductId.ProductName;
                if (stocks.Count() > 0)
                {
                    var stocksvm = from s in stocks
                                   select new JmInventoryTStockViewModel
                                   {
                                       StockSn = s.StockSn,
                                       ProductIdName = productname,
                                       StockId = s.Id,
                                       StockOrderNo = s.StockOrderNo
                                   };
                    liststockvm = stocksvm.ToList();
                }
                else
                {
                    JmInventoryTStockViewModel stock;
                    for (int i = 0; i < det.TransDetQty; i++)
                    {
                        stock = new JmInventoryTStockViewModel();
                        stock.StockId = Guid.NewGuid().ToString();
                        stock.StockSn = string.Empty;
                        stock.ProductIdName = productname;
                        stock.StockOrderNo = (i + 1);
                        liststockvm.Add(stock);
                    }
                }
            }
            vm.Stocks = liststockvm;
            return PartialView(vm);
        }

        [HttpPost]
        public ActionResult ViewSNContent_Update(TStockInputViewModel model)
        {
            if (model != null)
            {
                JmInventoryTStockViewModel vm;
                JmInventoryTStock stock;
                JmInventoryTTransDet det = _IJmInventoryTTransDetTasks.One(model.TransDetId);
                bool isInsert = false;
                for (int i = 0; i < model.Stocks.Count; i++)
                {
                    vm = model.Stocks[i];
                    stock = _tasks.One(vm.StockId); ;

                    if (stock != null)
                    {
                        stock.ModifiedDate = DateTime.Now;
                        stock.ModifiedBy = User.Identity.Name;
                        stock.DataStatus = EnumDataStatus.Updated.ToString();
                        isInsert = false;
                    }
                    else
                    {
                        stock = new JmInventoryTStock();
                        stock.SetAssignedIdTo(Guid.NewGuid().ToString());
                        stock.CreatedDate = DateTime.Now;
                        stock.CreatedBy = User.Identity.Name;
                        stock.DataStatus = EnumDataStatus.New.ToString();
                        isInsert = true;
                    }
                    stock.StockOrderNo = vm.StockOrderNo;
                    stock.StockSn = vm.StockSn;
                    stock.StockInTransId = model.TransDetId;
                    stock.StockInDate = DateTime.Now;
                    stock.ProductId = det.ProductId;
                    if (isInsert)
                    {
                        _tasks.Insert(stock);
                    }
                    else
                    {
                        _tasks.Update(stock);
                    }
                }
            }
            return PartialView(model);
        }
    }
}
