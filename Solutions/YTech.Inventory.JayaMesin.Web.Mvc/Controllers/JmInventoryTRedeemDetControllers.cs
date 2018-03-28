using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using System.Web.Mvc;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;
using YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers.ViewModels;
using System.Linq;
using YTech.ServiceTracker.JayaMesin.Domain;
using System;
using System.Web;
using YTech.ServiceTracker.JayaMesin.Enums;

namespace YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers
{
    [HandleError]
    [Authorize]
    public partial class JmInventoryTRedeemDetController : Controller
    {
       private readonly IJmInventoryTRedeemDetTasks _tasks;
        public JmInventoryTRedeemDetController(IJmInventoryTRedeemDetTasks tasks)
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

        public ActionResult JmInventoryTRedeemDets_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetJmInventoryTRedeemDets().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryTRedeemDets_Create([DataSourceRequest] DataSourceRequest request, JmInventoryTRedeemDetViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                JmInventoryTRedeemDet entity = new JmInventoryTRedeemDet();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToJmInventoryTRedeemDet(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToJmInventoryTRedeemDet(JmInventoryTRedeemDetViewModel vm, JmInventoryTRedeemDet entity)
        {
            entity.RedeemId = string.IsNullOrEmpty(vm.RedeemId) ? null : _JmInventoryTRedeemTasks.One(vm.RedeemId);
            entity.StockId = string.IsNullOrEmpty(vm.StockId) ? null : _JmInventoryTStockTasks.One(vm.StockId);
            
            entity.RedeemDetValue = vm.RedeemDetValue;
            entity.RedeemDetStatus = vm.RedeemDetStatus;
            entity.RedeemDetDesc = vm.RedeemDetDesc;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryTRedeemDets_Update([DataSourceRequest] DataSourceRequest request, JmInventoryTRedeemDetViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.RedeemDetId);
                if (entity != null)
                {
                    ConvertToJmInventoryTRedeemDet(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryTRedeemDets_Destroy([DataSourceRequest] DataSourceRequest request, JmInventoryTRedeemDetViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.RedeemDetId);
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

        private IEnumerable<JmInventoryTRedeemDetViewModel> GetJmInventoryTRedeemDets()
        {
            var entitys = this._tasks.GetListNotDeleted();

            return from entity in entitys
                   select new JmInventoryTRedeemDetViewModel
        {
            RedeemId = entity.RedeemId != null ? entity.RedeemId.Id : string.Empty,
            RedeemIdName = entity.RedeemId != null ? entity.RedeemId.Name : string.Empty,
            StockId = entity.StockId != null ? entity.StockId.Id : string.Empty,
            StockIdName = entity.StockId != null ? entity.StockId.Name : string.Empty,
            
            RedeemDetValue = entity.RedeemDetValue,
            RedeemDetStatus = entity.RedeemDetStatus,
            RedeemDetDesc = entity.RedeemDetDesc,
            RedeemDetId = entity.Id
        };

        }
        
        public JsonResult PopulateJmInventoryTRedeemDets()
        {
            var list = from ent in _tasks.GetListNotDeleted()
                            select new
                                    {
                                        Id = ent.Id,
                                        RedeemDetIdName = ent.RedeemDetIdName
                                    };
            ViewData["JmInventoryTRedeemDets"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
