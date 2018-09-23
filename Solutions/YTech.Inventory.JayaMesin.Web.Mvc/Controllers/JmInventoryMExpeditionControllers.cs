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
    public partial class JmInventoryMExpeditionController : Controller
    {
       private readonly IJmInventoryMExpeditionTasks _tasks;
        public JmInventoryMExpeditionController(IJmInventoryMExpeditionTasks tasks)
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

        public ActionResult JmInventoryMExpeditions_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetJmInventoryMExpeditions().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryMExpeditions_Create([DataSourceRequest] DataSourceRequest request, JmInventoryMExpeditionViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                JmInventoryMExpedition entity = new JmInventoryMExpedition();
                entity.SetAssignedIdTo(vm.ExpeditionId);

                ConvertToJmInventoryMExpedition(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToJmInventoryMExpedition(JmInventoryMExpeditionViewModel vm, JmInventoryMExpedition entity)
        {
            
            entity.ExpeditionName = vm.ExpeditionName;
            entity.ExpeditionDesc = vm.ExpeditionDesc;
            entity.ExpeditionImgUrl = vm.ExpeditionImgUrl;
            entity.ExpeditionImg = UploadFiles(UploadFolder.Brand, vm.ExpeditionImgUrl);
            entity.ExpeditionAddress = vm.ExpeditionAddress;
            entity.ExpeditionPhone = vm.ExpeditionPhone;
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
        public ActionResult JmInventoryMExpeditions_Update([DataSourceRequest] DataSourceRequest request, JmInventoryMExpeditionViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.ExpeditionId);
                if (entity != null)
                {
                    ConvertToJmInventoryMExpedition(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryMExpeditions_Destroy([DataSourceRequest] DataSourceRequest request, JmInventoryMExpeditionViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.ExpeditionId);
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

        private IEnumerable<JmInventoryMExpeditionViewModel> GetJmInventoryMExpeditions()
        {
            var entitys = this._tasks.GetListNotDeleted();

            return from entity in entitys
                   select new JmInventoryMExpeditionViewModel
        {
            
            ExpeditionName = entity.ExpeditionName,
            ExpeditionDesc = entity.ExpeditionDesc,
            ExpeditionImg = entity.ExpeditionImg,
            ExpeditionImgUrl = entity.ExpeditionImgUrl,
            ExpeditionId = entity.Id,
            ExpeditionAddress = entity.ExpeditionAddress,
            ExpeditionPhone = entity.ExpeditionPhone
        };

        }
        
        public JsonResult PopulateJmInventoryMExpeditions()
        {
            var list = from ent in _tasks.GetListNotDeleted()
                            select new
                                    {
                                        Id = ent.Id,
                                        ExpeditionName = ent.ExpeditionName
                                    };
            ViewData["JmInventoryMExpeditions"] = list;
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

        public ActionResult GetLastCreatedExpedition(string random)
        {
            JmInventoryMExpedition ent = _tasks.GetLastCreatedExpedition();
            return Content(ent.Id);
        }
    }
}
