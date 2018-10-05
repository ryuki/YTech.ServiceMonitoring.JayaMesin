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
    public partial class JmInventoryMBrandController : Controller
    {
        private readonly IJmInventoryMBrandTasks _tasks;
        public JmInventoryMBrandController(IJmInventoryMBrandTasks tasks)
        {
            this._tasks = tasks;
        }

        [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index(bool? isModal)
        {
            if (isModal.HasValue)
                if (isModal.HasValue)
                    return View("Index", "~/Views/Shared/_NoMenuLayout.cshtml");

            return View();
        }

        public ActionResult JmInventoryMBrands_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetJmInventoryMBrands().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryMBrands_Create([DataSourceRequest] DataSourceRequest request, JmInventoryMBrandViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                JmInventoryMBrand entity = new JmInventoryMBrand();
                entity.SetAssignedIdTo(vm.BrandId);

                ConvertToJmInventoryMBrand(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToJmInventoryMBrand(JmInventoryMBrandViewModel vm, JmInventoryMBrand entity)
        {

            entity.BrandName = vm.BrandName;
            entity.BrandDesc = vm.BrandDesc;
            entity.BrandImgUrl = vm.BrandImgUrl;

            entity.BrandImg = UploadFiles(UploadFolder.Brand, vm.BrandImgUrl);
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
        public ActionResult JmInventoryMBrands_Update([DataSourceRequest] DataSourceRequest request, JmInventoryMBrandViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.BrandId);
                if (entity != null)
                {
                    ConvertToJmInventoryMBrand(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryMBrands_Destroy([DataSourceRequest] DataSourceRequest request, JmInventoryMBrandViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.BrandId);
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

        private IEnumerable<JmInventoryMBrandViewModel> GetJmInventoryMBrands()
        {
            var entitys = this._tasks.GetListNotDeleted();

            return from entity in entitys
                   select new JmInventoryMBrandViewModel
        {

            BrandName = entity.BrandName,
            BrandDesc = entity.BrandDesc,
            BrandImg = entity.BrandImg,
            BrandImgUrl = entity.BrandImgUrl,
            BrandId = entity.Id
        };

        }

        public JsonResult PopulateJmInventoryMBrands()
        {
            var list = from ent in _tasks.GetListNotDeleted()
                       select new
                               {
                                   Id = ent.Id,
                                   BrandName = ent.BrandName
                               };
            ViewData["JmInventoryMBrands"] = list;
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

        public ActionResult GetLastCreatedBrand(string random)
        {
            JmInventoryMBrand ent = _tasks.GetLastCreatedBrand();
            return Content(ent.Id);
        }
    }
}
