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
        public ActionResult ReceiptDetail_Read(string TransId, [DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetReceiptDets(TransId).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReceiptDetail_Create(string TransId, [DataSourceRequest] DataSourceRequest request, TransReceiptDetViewModel vm)
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

        private void ConvertToJmInventoryTTransDet(TransReceiptDetViewModel vm, JmInventoryTTransDet entity)
        {
            entity.ProductId = string.IsNullOrEmpty(vm.ProductId) ? null : _JmInventoryMProductTasks.One(vm.ProductId);            
            entity.TransDetQty = vm.TransDetQty;
            entity.TransDetStatus = vm.TransDetStatus;
            entity.TransDetDesc = vm.TransDetDesc;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReceiptDetail_Update(string TransId, [DataSourceRequest] DataSourceRequest request, TransReceiptDetViewModel vm)
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
        public ActionResult ReceiptDetail_Destroy(string TransId, [DataSourceRequest] DataSourceRequest request, TransReceiptDetViewModel vm)
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

        private IEnumerable<TransReceiptDetViewModel> GetReceiptDets(string TransId)
        {
            var entitys = this._tasks.GetListByTransId(TransId);

            return from entity in entitys
                   select new TransReceiptDetViewModel
        {
            ProductId = entity.ProductId != null ? entity.ProductId.Id : string.Empty,
            ProductName = entity.ProductId != null ? entity.ProductId.ProductName : string.Empty,
            
            TransDetQty = entity.TransDetQty,
            TransDetStatus = entity.TransDetStatus,
            TransDetDesc = entity.TransDetDesc,
            TransDetId = entity.Id
        };

        }
    }
}
