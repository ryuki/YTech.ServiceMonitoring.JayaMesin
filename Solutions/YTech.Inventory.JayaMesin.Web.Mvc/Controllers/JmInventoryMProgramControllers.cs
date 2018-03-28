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
    public partial class JmInventoryMProgramController : Controller
    {
       private readonly IJmInventoryMProgramTasks _tasks;
        public JmInventoryMProgramController(IJmInventoryMProgramTasks tasks)
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

            PopulateProgramStatus();
            return View();
        }

        private void PopulateProgramStatus()
        {
            var trans_status = from EnumProgramStatus e in Enum.GetValues(typeof(EnumProgramStatus))
                               select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["program_status"] = trans_status;
        }

        public ActionResult JmInventoryMPrograms_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetJmInventoryMPrograms().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryMPrograms_Create([DataSourceRequest] DataSourceRequest request, JmInventoryMProgramViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                JmInventoryMProgram entity = new JmInventoryMProgram();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToJmInventoryMProgram(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToJmInventoryMProgram(JmInventoryMProgramViewModel vm, JmInventoryMProgram entity)
        {
            
            entity.ProgramName = vm.ProgramName;
            entity.ProgramStatus = vm.ProgramStatus;
            entity.ProgramExpiredDate = vm.ProgramExpiredDate;
            entity.ProgramType = vm.ProgramType;
            entity.ProgramDesc = vm.ProgramDesc;
            entity.ProgramImgUrl = vm.ProgramImgUrl;

            entity.ProgramImg = UploadFiles(UploadFolder.Program, vm.ProgramImgUrl);
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
        public ActionResult JmInventoryMPrograms_Update([DataSourceRequest] DataSourceRequest request, JmInventoryMProgramViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.ProgramId);
                if (entity != null)
                {
                    ConvertToJmInventoryMProgram(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JmInventoryMPrograms_Destroy([DataSourceRequest] DataSourceRequest request, JmInventoryMProgramViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.ProgramId);
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

        private IEnumerable<JmInventoryMProgramViewModel> GetJmInventoryMPrograms()
        {
            var entitys = this._tasks.GetListNotDeleted();

            return from entity in entitys
                   select new JmInventoryMProgramViewModel
        {
            
            ProgramName = entity.ProgramName,
            ProgramStatus = entity.ProgramStatus,
            ProgramExpiredDate = entity.ProgramExpiredDate,
            ProgramType = entity.ProgramType,
            ProgramDesc = entity.ProgramDesc,
            ProgramId = entity.Id,
            ProgramImgUrl = entity.ProgramImgUrl
        };

        }
        
        public JsonResult PopulateJmInventoryMPrograms()
        {
            var list = from ent in _tasks.GetListNotDeleted()
                            select new
                                    {
                                        Id = ent.Id,
                                        ProgramName = ent.ProgramName
                                    };
            ViewData["JmInventoryMPrograms"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            return CommonUploadImage(file, UploadFolder.Program);
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
