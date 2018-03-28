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
    public partial class JmInventoryTRedeemController : Controller
    {
       private readonly IJmInventoryTRedeemTasks _tasks;
        public JmInventoryTRedeemController(IJmInventoryTRedeemTasks tasks)
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

        public ActionResult JmInventoryTRedeems_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetJmInventoryTRedeems().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryTRedeems_Create([DataSourceRequest] DataSourceRequest request, JmInventoryTRedeemViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                JmInventoryTRedeem entity = new JmInventoryTRedeem();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToJmInventoryTRedeem(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToJmInventoryTRedeem(JmInventoryTRedeemViewModel vm, JmInventoryTRedeem entity)
        {
            entity.ProgramId = string.IsNullOrEmpty(vm.ProgramId) ? null : _JmInventoryMProgramTasks.One(vm.ProgramId);
            
            entity.RedeemType = vm.RedeemType;
            entity.RedeemDate = vm.RedeemDate;
            entity.RedeemValueTotal = vm.RedeemValueTotal;
            entity.RedeemStatus = vm.RedeemStatus;
            entity.RedeemDateOut = vm.RedeemDateOut;
            entity.RedeemDateCair = vm.RedeemDateCair;
            entity.CustomerId = vm.CustomerId;
            entity.RedeemDesc = vm.RedeemDesc;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryTRedeems_Update([DataSourceRequest] DataSourceRequest request, JmInventoryTRedeemViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.RedeemId);
                if (entity != null)
                {
                    ConvertToJmInventoryTRedeem(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryTRedeems_Destroy([DataSourceRequest] DataSourceRequest request, JmInventoryTRedeemViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.RedeemId);
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

        private IEnumerable<JmInventoryTRedeemViewModel> GetJmInventoryTRedeems()
        {
            var entitys = this._tasks.GetListNotDeleted();

            return from entity in entitys
                   select new JmInventoryTRedeemViewModel
        {
            ProgramId = entity.ProgramId != null ? entity.ProgramId.Id : string.Empty,
            ProgramIdName = entity.ProgramId != null ? entity.ProgramId.Name : string.Empty,
            
            RedeemType = entity.RedeemType,
            RedeemDate = entity.RedeemDate,
            RedeemValueTotal = entity.RedeemValueTotal,
            RedeemStatus = entity.RedeemStatus,
            RedeemDateOut = entity.RedeemDateOut,
            RedeemDateCair = entity.RedeemDateCair,
            CustomerId = entity.CustomerId,
            RedeemDesc = entity.RedeemDesc,
            RedeemId = entity.Id
        };

        }
        
        public JsonResult PopulateJmInventoryTRedeems()
        {
            var list = from ent in _tasks.GetListNotDeleted()
                            select new
                                    {
                                        Id = ent.Id,
                                        RedeemIdName = ent.RedeemIdName
                                    };
            ViewData["JmInventoryTRedeems"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
