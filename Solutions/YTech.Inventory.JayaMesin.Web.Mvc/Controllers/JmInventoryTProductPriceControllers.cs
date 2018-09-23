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
using System.IO;

namespace YTech.Inventory.JayaMesin.Web.Mvc.Controllers
{
    [HandleError]
    [Authorize]
    public partial class JmInventoryTProductPriceController : Controller
    {
        private readonly IJmInventoryTProductPriceTasks _tasks;
        private readonly IJmInventoryMProductTasks _JmInventoryMProductTasks;
        private readonly IJmInventoryMSupplierTasks _JmInventoryMSupplierTasks;
        public JmInventoryTProductPriceController(IJmInventoryTProductPriceTasks tasks, IJmInventoryMProductTasks _JmInventoryMProductTasks, IJmInventoryMSupplierTasks _JmInventoryMSupplierTasks)
        {
            this._tasks = tasks;
            this._JmInventoryMProductTasks = _JmInventoryMProductTasks;
            this._JmInventoryMSupplierTasks = _JmInventoryMSupplierTasks;
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

        public ActionResult JmInventoryTProductPrices_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetJmInventoryTProductPrices().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryTProductPrices_Create([DataSourceRequest] DataSourceRequest request, JmInventoryTProductPriceViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                JmInventoryTProductPrice entity = new JmInventoryTProductPrice();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToJmInventoryTProductPrice(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToJmInventoryTProductPrice(JmInventoryTProductPriceViewModel vm, JmInventoryTProductPrice entity)
        {
            entity.ProductId = string.IsNullOrEmpty(vm.ProductId) ? null : _JmInventoryMProductTasks.One(vm.ProductId);
            entity.SupplierId = string.IsNullOrEmpty(vm.SupplierId) ? null : _JmInventoryMSupplierTasks.One(vm.SupplierId);
            
            entity.ProductPrice = vm.ProductPrice;
            entity.ProductPriceStatus = vm.ProductPriceStatus;
            entity.ProductPriceDesc = vm.ProductPriceDesc;

            entity.ProductPriceImgUrl = vm.ProductPriceImgUrl;

            entity.ProductPriceImg = UploadFiles(UploadFolder.Product, vm.ProductPriceImgUrl);
        }

        public byte[] UploadFiles(string UploadFolder, string imageFileName)
        {
            if (!string.IsNullOrEmpty(imageFileName))
            {
                var physicalPath = Path.Combine(Server.MapPath(UploadFolder), imageFileName);
                // Load file meta data with FileInfo
                FileInfo fileInfo = new FileInfo(physicalPath);

                // The byte[] to save the data in
                byte[] data = new byte[fileInfo.Length];

                // Load a filestream and put its content into the byte[]
                using (FileStream fs = fileInfo.OpenRead())
                {
                    fs.Read(data, 0, data.Length);
                }
                return data;
            }
            else
            {
                return null;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryTProductPrices_Update([DataSourceRequest] DataSourceRequest request, JmInventoryTProductPriceViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.ProductPriceId);
                if (entity != null)
                {
                    ConvertToJmInventoryTProductPrice(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryTProductPrices_Destroy([DataSourceRequest] DataSourceRequest request, JmInventoryTProductPriceViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.ProductPriceId);
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

        private IEnumerable<JmInventoryTProductPriceViewModel> GetJmInventoryTProductPrices()
        {
            var entitys = this._tasks.GetListNotDeleted();

            return from entity in entitys
                   select new JmInventoryTProductPriceViewModel
        {
            ProductId = entity.ProductId != null ? entity.ProductId.Id : string.Empty,
            ProductIdName = entity.ProductId != null ? entity.ProductId.ProductName : string.Empty,
            SupplierId = entity.SupplierId != null ? entity.SupplierId.Id : string.Empty,
            SupplierIdName = entity.SupplierId != null ? entity.SupplierId.SupplierName : string.Empty,
            
            ProductPrice = entity.ProductPrice,
            ProductPriceStatus = entity.ProductPriceStatus,
            ProductPriceDesc = entity.ProductPriceDesc,
            ProductPriceId = entity.Id,
            ProductPriceImgUrl = entity.ProductPriceImgUrl
        };

        }
        
        //public JsonResult PopulateJmInventoryTProductPrices()
        //{
        //    var list = from ent in _tasks.GetListNotDeleted()
        //                    select new
        //                            {
        //                                Id = ent.Id,
        //                                ProductPriceIdName = ent.ProductPriceIdName
        //                            };
        //    ViewData["JmInventoryTProductPrices"] = list;
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}


        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            return CommonUploadImage(file, UploadFolder.Product);
        }

        public ActionResult CommonUploadImage(HttpPostedFileBase file, string UploadFolder)
        {
            var fileName = Path.GetFileName(file.FileName);
            var ext = Path.GetExtension(file.FileName);
            string newFileName = Guid.NewGuid().ToString() + ext;
            var physicalPath = Path.Combine(Server.MapPath(UploadFolder), newFileName);

            file.SaveAs(physicalPath);

            string fullImageUrl = Url.Content(UploadFolder + newFileName);

            return Json(new { ImageUrl = newFileName, FullImageUrl = fullImageUrl }, "text/plain");
        }
    }
}
