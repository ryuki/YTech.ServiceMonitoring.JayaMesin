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
    public partial class JmInventoryTTransDetController : Controller
    {
        private readonly IJmInventoryTTransDetTasks _tasks;
        private readonly IJmInventoryMProductTasks _JmInventoryMProductTasks;
        private readonly IJmInventoryTTranTasks _JmInventoryTTranTasks;
        public JmInventoryTTransDetController(IJmInventoryTTransDetTasks tasks, IJmInventoryMProductTasks _JmInventoryMProductTasks, IJmInventoryTTranTasks _JmInventoryTTranTasks)
        {
            this._tasks = tasks;
            this._JmInventoryMProductTasks = _JmInventoryMProductTasks;
            this._JmInventoryTTranTasks = _JmInventoryTTranTasks;
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

        public ActionResult PODetail_Read(string TransId, [DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetJmInventoryTTransDets(TransId).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PODetail_Create(string TransId, [DataSourceRequest] DataSourceRequest request, TransPODetViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                JmInventoryTTransDet entity = new JmInventoryTTransDet();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                entity.TransId = string.IsNullOrEmpty(TransId) ? null : _JmInventoryTTranTasks.One(TransId);

                ConvertToJmInventoryTTransDet(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToJmInventoryTTransDet(TransPODetViewModel vm, JmInventoryTTransDet entity)
        {
            entity.ProductId = string.IsNullOrEmpty(vm.ProductId) ? null : _JmInventoryMProductTasks.One(vm.ProductId);
            entity.TransDetQty = vm.TransDetQty;
            entity.TransDetPrice = vm.TransDetPrice;
            entity.TransDetDisc = vm.TransDetDisc;
            entity.TransDetIsTaxed = vm.TransDetIsTaxed;

            decimal? qty = 0;
            decimal? price = 0;
            decimal? disc = 0;
            qty = vm.TransDetQty.HasValue ? vm.TransDetQty.Value : 0;
            price = vm.TransDetPrice.HasValue ? vm.TransDetPrice.Value : 0;
            disc = vm.TransDetDisc.HasValue ? vm.TransDetDisc.Value : 0;
            decimal? subtotal = (qty * price) * (1 - (disc / 100));

            entity.TransDetTotal = subtotal;
            entity.TransDetStatus = vm.TransDetStatus;
            entity.TransDetDesc = vm.TransDetDesc;
            entity.TransDetOngkir = vm.TransDetOngkir;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PODetail_Update(string TransId, [DataSourceRequest] DataSourceRequest request, TransPODetViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.TransDetId);
                if (entity != null)
                {
                    ConvertToJmInventoryTTransDet(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PODetail_Destroy(string TransId, [DataSourceRequest] DataSourceRequest request, TransPODetViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.TransDetId);
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

        private IEnumerable<TransPODetViewModel> GetJmInventoryTTransDets(string TransId)
        {
            var entitys = this._tasks.GetListByTransId(TransId);

            return from entity in entitys
                   select new TransPODetViewModel
        {
            ProductId = entity.ProductId != null ? entity.ProductId.Id : string.Empty,
            ProductName = entity.ProductId != null ? entity.ProductId.ProductName : string.Empty,

            TransDetQty = entity.TransDetQty,
            TransDetPrice = entity.TransDetPrice,
            TransDetDisc = entity.TransDetDisc,
            TransDetTotal = entity.TransDetTotal,
            TransDetStatus = entity.TransDetStatus,
            TransDetDesc = entity.TransDetDesc,
            TransDetId = entity.Id,
            TransDetOngkir = entity.TransDetOngkir,
            TransDetIsTaxed = entity.TransDetIsTaxed.HasValue ? entity.TransDetIsTaxed.Value : false
        };

        }

        public JsonResult PopulateJmInventoryTTransDets()
        {
            var list = from ent in _tasks.GetListNotDeleted()
                       select new
                               {
                                   Id = ent.Id,
                                   TransDetIdName = ent.TransDetDesc
                               };
            ViewData["JmInventoryTTransDets"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
