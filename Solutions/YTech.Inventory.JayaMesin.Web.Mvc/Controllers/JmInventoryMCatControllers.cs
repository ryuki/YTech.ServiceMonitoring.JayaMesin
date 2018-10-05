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
    public partial class JmInventoryMCatController : Controller
    {
        private readonly IJmInventoryMCatTasks _tasks;
        public JmInventoryMCatController(IJmInventoryMCatTasks tasks)
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

        public ActionResult JmInventoryMCats_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetJmInventoryMCats().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryMCats_Create([DataSourceRequest] DataSourceRequest request, JmInventoryMCatViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                JmInventoryMCat entity = new JmInventoryMCat();
                entity.SetAssignedIdTo(vm.CatId);

                ConvertToJmInventoryMCat(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToJmInventoryMCat(JmInventoryMCatViewModel vm, JmInventoryMCat entity)
        {

            entity.CatParentId = vm.CatParentId;
            entity.CatName = vm.CatName;
            entity.CatDesc = vm.CatDesc;
            entity.CatImgUrl = vm.CatImgUrl;

            entity.CatImg = UploadFiles(UploadFolder.Cat, vm.CatImgUrl);
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
        public ActionResult JmInventoryMCats_Update([DataSourceRequest] DataSourceRequest request, JmInventoryMCatViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.CatId);
                if (entity != null)
                {
                    ConvertToJmInventoryMCat(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryMCats_Destroy([DataSourceRequest] DataSourceRequest request, JmInventoryMCatViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.CatId);
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

        private IEnumerable<JmInventoryMCatViewModel> GetJmInventoryMCats()
        {
            var entitys = this._tasks.GetListNotDeleted();

            return from entity in entitys
                   select new JmInventoryMCatViewModel
        {

            CatParentId = entity.CatParentId,
            CatName = entity.CatName,
            CatDesc = entity.CatDesc,
            CatImg = entity.CatImg,
            CatImgUrl = entity.CatImgUrl,
            CatId = entity.Id
        };

        }

        public JsonResult PopulateJmInventoryMCats()
        {
            var list = from ent in _tasks.GetListNotDeleted()
                       select new
                               {
                                   Id = ent.Id,
                                   CatName = ent.CatName
                               };
            ViewData["JmInventoryMCats"] = list;
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

        public ActionResult GetLastCreatedCat(string random)
        {
            JmInventoryMCat ent = _tasks.GetLastCreatedCat();
            return Content(ent.Id);
        }
    }
}
