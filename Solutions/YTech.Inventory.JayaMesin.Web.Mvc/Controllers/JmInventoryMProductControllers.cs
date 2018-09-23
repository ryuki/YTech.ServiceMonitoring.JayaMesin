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
    public partial class JmInventoryMProductController : Controller
    {
        private readonly IJmInventoryMProductTasks _tasks;
        private readonly IJmInventoryMBrandTasks _brandTasks;
        private readonly IJmInventoryMCatTasks _catTasks;
        private readonly IJmInventoryTProductPriceTasks _JmInventoryTProductPriceTasks;
        private readonly IJmInventoryMSupplierTasks _IJmInventoryMSupplierTasks;

        public JmInventoryMProductController(IJmInventoryMProductTasks tasks, IJmInventoryMBrandTasks brandTasks, IJmInventoryMCatTasks catTasks, IJmInventoryTProductPriceTasks _JmInventoryTProductPriceTasks, IJmInventoryMSupplierTasks _IJmInventoryMSupplierTasks)
        {
            this._tasks = tasks;
            this._brandTasks = brandTasks;
            this._catTasks = catTasks;
            this._JmInventoryTProductPriceTasks = _JmInventoryTProductPriceTasks;
            this._IJmInventoryMSupplierTasks = _IJmInventoryMSupplierTasks;
        }

        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS, SALES")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index(bool? isModal)
        {
            if (isModal.HasValue)
                if (isModal.HasValue)
                    return View("Index", "~/Views/Shared/_NoMenuLayout.cshtml");

            PopulateProductStatus();
            PopulatePriceStatus();

            return View();
        }

        public JsonResult PopulateProductStatus()
        {
            var trans_status = from EnumProductStatus e in Enum.GetValues(typeof(EnumProductStatus))
                               select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["product_status"] = trans_status;
            return Json(trans_status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JmInventoryMProducts_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetJmInventoryMProducts().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryMProducts_Create([DataSourceRequest] DataSourceRequest request, JmInventoryMProductViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                JmInventoryMProduct entity = new JmInventoryMProduct();
                entity.SetAssignedIdTo(vm.ProductId);

                ConvertToJmInventoryMProduct(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToJmInventoryMProduct(JmInventoryMProductViewModel vm, JmInventoryMProduct entity)
        {

            entity.ProductName = vm.ProductName;
            entity.CatId = _catTasks.One(vm.CatId);
            entity.BrandId = _brandTasks.One(vm.BrandId);
            entity.ProductStatus = vm.ProductStatus;
            entity.ProductDesc = vm.ProductDesc;
            entity.ProductImgUrl = vm.ProductImgUrl;

            entity.ProductImg = UploadFiles(UploadFolder.Product, vm.ProductImgUrl);

            entity.ProductLastPrice = vm.ProductLastPrice;
            entity.ProductEstStock = vm.ProductEstStock;
            entity.ProductEstStockDate = vm.ProductEstStockDate;
            entity.ProductMinStock = vm.ProductMinStock;
            entity.ProductPriceSales = vm.ProductPriceSales;
            entity.ProductLastPriceDate = vm.ProductLastPriceDate;
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
        public ActionResult JmInventoryMProducts_Update([DataSourceRequest] DataSourceRequest request, JmInventoryMProductViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.ProductId);
                if (entity != null)
                {
                    ConvertToJmInventoryMProduct(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryMProducts_Destroy([DataSourceRequest] DataSourceRequest request, JmInventoryMProductViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.ProductId);
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

        private IEnumerable<JmInventoryMProductViewModel> GetJmInventoryMProducts()
        {
            var entitys = this._tasks.GetListNotDeleted();

            return from entity in entitys
                   select new JmInventoryMProductViewModel
        {

            ProductName = entity.ProductName,
            CatId = entity.CatId != null ? entity.CatId.Id : string.Empty,
            CatName = entity.CatId != null ? entity.CatId.CatName : string.Empty,
            BrandId = entity.BrandId != null ? entity.BrandId.Id : string.Empty,
            BrandName = entity.BrandId != null ? entity.BrandId.BrandName : string.Empty,
            ProductStatus = entity.ProductStatus,
            ProductDesc = entity.ProductDesc,
            ProductId = entity.Id,
            ProductImgUrl = entity.ProductImgUrl,
            ProductLastPrice = entity.ProductLastPrice,
            ProductEstStock = entity.ProductEstStock,
            ProductEstStockDate = entity.ProductEstStockDate,
            ProductMinStock = entity.ProductMinStock,
            ProductPriceSales = entity.ProductPriceSales,
            ProductLastPriceDate = entity.ProductLastPriceDate
        };

        }

        public JsonResult PopulateProducts()
        {
            var list = from ent in _tasks.GetListNotDeletedAndNotDisc()
                       select new
                               {
                                   Id = ent.Id,
                                   ProductName = ent.ProductName
                               };
            ViewData["Products"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            return CommonUploadImage(file, UploadFolder.Product);
        }

        public ActionResult UploadImagePrice(HttpPostedFileBase filePrice)
        {
            return CommonUploadImage(filePrice, UploadFolder.Product);
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

        public ActionResult GetLastCreatedProduct(string random)
        {
            JmInventoryMProduct supp = _tasks.GetLastCreatedProduct();
            return Content(supp.Id);
        }

        public ActionResult GetProductDetail(string random, string productId)
        {
            JmInventoryMProduct supp = _tasks.One(productId);
            var list = new
                       {
                           Id = supp.Id,
                           ProductName = supp.ProductName,
                           ProductLastPrice = supp.ProductLastPrice
                       };
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #region product price

        public ActionResult JmInventoryTProductPrices_Read([DataSourceRequest] DataSourceRequest request, string ParentProductId)
        {
            return Json(GetJmInventoryTProductPrices(ParentProductId).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryTProductPrices_Create([DataSourceRequest] DataSourceRequest request, JmInventoryTProductPriceViewModel vm, string ParentProductId)
        {
            if (vm != null && ModelState.IsValid)
            {
                JmInventoryTProductPrice entity = new JmInventoryTProductPrice();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToJmInventoryTProductPrice(vm, entity, ParentProductId);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _JmInventoryTProductPriceTasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToJmInventoryTProductPrice(JmInventoryTProductPriceViewModel vm, JmInventoryTProductPrice entity, string ParentProductId)
        {
            entity.ProductId = string.IsNullOrEmpty(ParentProductId) ? null : _tasks.One(ParentProductId);
            entity.SupplierId = string.IsNullOrEmpty(vm.SupplierId) ? null : _IJmInventoryMSupplierTasks.One(vm.SupplierId);

            entity.ProductPrice = vm.ProductPrice;
            entity.ProductPriceStatus = vm.ProductPriceStatus;
            entity.ProductPriceDesc = vm.ProductPriceDesc;
            entity.ProductPriceDate = vm.ProductPriceDate;
            entity.ProductPriceImgUrl = vm.ProductPriceImgUrl;

            entity.ProductPriceImg = UploadFiles(UploadFolder.Product, vm.ProductPriceImgUrl);
            entity.ProductPriceQty = vm.ProductPriceQty;
            entity.ProductPriceOngkir = vm.ProductPriceOngkir;

            //update price to last price
            if (vm.ProductPriceStatus == EnumPriceStatus.PO.ToString())
            {
                decimal purchasePrice = entity.ProductPrice.HasValue ? entity.ProductPrice.Value : 0;
                decimal priceOngkir = entity.ProductPriceOngkir.HasValue ? entity.ProductPriceOngkir.Value : 0;
                UpdatePrice(entity.ProductId, purchasePrice, priceOngkir, entity.ProductPriceDate);
            }
        }

        private void UpdatePrice(JmInventoryMProduct product, decimal purchasePrice, decimal priceOngkir,DateTime? productPriceDate)
        {
            if (product != null)
            {
                product.ProductLastPriceDate = productPriceDate;
                product.ProductLastPrice = purchasePrice;
                product.ProductPriceSales = (purchasePrice + priceOngkir) * (decimal)1.02;
                product.ModifiedDate = DateTime.Now;
                product.ModifiedBy = User.Identity.Name;
                product.DataStatus = EnumDataStatus.Updated.ToString();
                _tasks.Update(product);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryTProductPrices_Update([DataSourceRequest] DataSourceRequest request, JmInventoryTProductPriceViewModel vm, string ParentProductId)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _JmInventoryTProductPriceTasks.One(vm.ProductPriceId);
                if (entity != null)
                {
                    ConvertToJmInventoryTProductPrice(vm, entity, ParentProductId);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _JmInventoryTProductPriceTasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryTProductPrices_Destroy([DataSourceRequest] DataSourceRequest request, JmInventoryTProductPriceViewModel vm, string ParentProductId)
        {
            if (vm != null)
            {
                var entity = _JmInventoryTProductPriceTasks.One(vm.ProductPriceId);
                if (entity != null)
                {
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Deleted.ToString();
                    _JmInventoryTProductPriceTasks.Update(entity);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<JmInventoryTProductPriceViewModel> GetJmInventoryTProductPrices(string ParentProductId)
        {
            var entitys = this._JmInventoryTProductPriceTasks.GetListByProductId(ParentProductId);

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
                       ProductPriceImgUrl = entity.ProductPriceImgUrl,
                       ProductPriceDate = entity.ProductPriceDate,
                       ProductPriceQty = entity.ProductPriceQty,
                       ProductPriceOngkir = entity.ProductPriceOngkir
                   };

        }

        private void PopulatePriceStatus()
        {
            var trans_status = from EnumPriceStatus e in Enum.GetValues(typeof(EnumPriceStatus))
                               select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["price_status"] = trans_status;
        }

        #endregion
    }
}
