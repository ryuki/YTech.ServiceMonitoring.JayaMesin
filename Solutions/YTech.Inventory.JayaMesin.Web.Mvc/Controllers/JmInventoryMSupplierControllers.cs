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
using System.Drawing;

namespace YTech.Inventory.JayaMesin.Web.Mvc.Controllers
{
    [HandleError]
    [Authorize]
    public partial class JmInventoryMSupplierController : Controller
    {
        private readonly IJmInventoryMSupplierTasks _tasks;
        public JmInventoryMSupplierController(IJmInventoryMSupplierTasks tasks)
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

            PopulateSupplierStatus();
            return View();
        }

        public JsonResult PopulateSupplierStatus()
        {
            var trans_status = from EnumSupplierStatus e in Enum.GetValues(typeof(EnumSupplierStatus))
                               select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["supplier_status"] = trans_status;
            return Json(trans_status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JmInventoryMSuppliers_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetJmInventoryMSuppliers().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryMSuppliers_Create([DataSourceRequest] DataSourceRequest request, JmInventoryMSupplierViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                JmInventoryMSupplier entity = new JmInventoryMSupplier();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToJmInventoryMSupplier(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToJmInventoryMSupplier(JmInventoryMSupplierViewModel vm, JmInventoryMSupplier entity)
        {

            entity.SupplierName = vm.SupplierName;
            entity.SupplierAddress = vm.SupplierAddress;
            entity.SupplierPhone = vm.SupplierPhone;
            entity.SupplierDebtTermin = vm.SupplierDebtTermin;
            entity.SupplierStatus = vm.SupplierStatus;
            entity.SupplierDesc = vm.SupplierDesc;
            entity.SupplierNpwp = vm.SupplierNpwp;
            entity.SupplierNpwpImgUrl = vm.SupplierNpwpImgUrl;

            entity.SupplierNpwpImg = UploadFiles(UploadFolder.Supplier, vm.SupplierNpwpImgUrl);
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
        public ActionResult JmInventoryMSuppliers_Update([DataSourceRequest] DataSourceRequest request, JmInventoryMSupplierViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.SupplierId);
                if (entity != null)
                {
                    ConvertToJmInventoryMSupplier(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryMSuppliers_Destroy([DataSourceRequest] DataSourceRequest request, JmInventoryMSupplierViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.SupplierId);
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

        private IEnumerable<JmInventoryMSupplierViewModel> GetJmInventoryMSuppliers()
        {
            var entitys = this._tasks.GetListNotDeleted();

            return from entity in entitys
                   select new JmInventoryMSupplierViewModel
        {

            SupplierName = entity.SupplierName,
            SupplierAddress = entity.SupplierAddress,
            SupplierPhone = entity.SupplierPhone,
            SupplierStatus = entity.SupplierStatus,
            SupplierDesc = entity.SupplierDesc,
            SupplierId = entity.Id,
            SupplierNpwp = entity.SupplierNpwp,
            SupplierNpwpImgUrl = entity.SupplierNpwpImgUrl,
            SupplierDebtTermin = entity.SupplierDebtTermin
        };

        }

        public JsonResult PopulateSuppliers()
        {
            var list = from ent in _tasks.GetListNotDeleted()
                       select new JmInventoryMSupplierViewModel
                               {
                                   SupplierId = ent.Id,
                                   SupplierName = ent.SupplierName
                               };
            ViewData["Supplier"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            return CommonUploadImage(file, UploadFolder.Supplier);
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

        public ActionResult GetLastCreatedSupplier(string random)
        {
            JmInventoryMSupplier supp = _tasks.GetLastCreatedSupplier();
            return Content(supp.Id);
        }
    }

}
