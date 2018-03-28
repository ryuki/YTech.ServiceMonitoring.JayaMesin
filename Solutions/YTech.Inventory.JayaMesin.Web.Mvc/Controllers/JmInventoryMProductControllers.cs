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
        public JmInventoryMProductController(IJmInventoryMProductTasks tasks)
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

            PopulateProductStatus();
            return View();
        }

        private void PopulateProductStatus()
        {
            var trans_status = from EnumProductStatus e in Enum.GetValues(typeof(EnumProductStatus))
                               select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["product_status"] = trans_status;
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
            entity.ProductType = vm.ProductType;
            entity.ProductMerk = vm.ProductMerk;
            entity.ProductStatus = vm.ProductStatus;
            entity.ProductDesc = vm.ProductDesc;
            entity.ProductImgUrl = vm.ProductImgUrl;

            entity.ProductImg = UploadFiles(UploadFolder.Product, vm.ProductImgUrl);
        }

        public byte[] UploadFiles(string UploadFolder, string imageFileName)
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
            ProductType = entity.ProductType,
            ProductMerk = entity.ProductMerk,
            ProductStatus = entity.ProductStatus,
            ProductDesc = entity.ProductDesc,
            ProductId = entity.Id,
            ProductImgUrl = entity.ProductImgUrl
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
