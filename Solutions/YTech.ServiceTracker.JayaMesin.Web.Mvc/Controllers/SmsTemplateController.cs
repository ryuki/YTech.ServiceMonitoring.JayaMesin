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
    public partial class SmsTemplateController : Controller
    {
        private readonly IMSmsTemplateTasks smsTemplateTasks;
        public SmsTemplateController(IMSmsTemplateTasks smsTemplateTasks)
        {
            this.smsTemplateTasks = smsTemplateTasks;
        }

        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index(bool? isModal)
        {
            if (isModal.HasValue)
                if (isModal.HasValue)
                    return View("Index", "~/Views/Shared/_NoMenuLayout.cshtml");

            PopulateWOStatus();
            return View();
        }

        private void PopulateWOStatus()
        {
            var wostatus = from EnumWOStatus e in Enum.GetValues(typeof(EnumWOStatus))
                           select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["wo_status"] = wostatus;
        }

        public ActionResult SmsTemplate_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetSmsTemplate().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SmsTemplate_Create([DataSourceRequest] DataSourceRequest request, SmsTemplateViewModel smsVM)
        {
            if (smsVM != null && ModelState.IsValid)
            {
                MSmsTemplate sms = new MSmsTemplate();
                sms.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToCustomer(smsVM, sms);

                sms.CreatedDate = DateTime.Now;
                sms.CreatedBy = User.Identity.Name;
                sms.DataStatus = "New";

                smsTemplateTasks.Insert(sms);
            }

            return Json(new[] { smsVM }.ToDataSourceResult(request, ModelState));
        }

        private static void ConvertToCustomer(SmsTemplateViewModel smsVM, MSmsTemplate sms)
        {
            sms.WOStatus = smsVM.WOStatus;
            sms.SmsTemplateText = smsVM.SmsTemplateText;
            sms.SmsTemplateStatus = smsVM.SmsTemplateStatus;
            sms.SmsTemplateDesc = smsVM.SmsTemplateDesc;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SmsTemplate_Update([DataSourceRequest] DataSourceRequest request, SmsTemplateViewModel smsVM)
        {
            if (smsVM != null && ModelState.IsValid)
            {
                var sms = smsTemplateTasks.One(smsVM.SmsTemplateID);
                if (sms != null)
                {
                    ConvertToCustomer(smsVM, sms);

                    sms.ModifiedDate = DateTime.Now;
                    sms.ModifiedBy = User.Identity.Name;
                    sms.DataStatus = "Updated";

                    smsTemplateTasks.Update(sms);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SmsTemplate_Destroy([DataSourceRequest] DataSourceRequest request, SmsTemplateViewModel smsVM)
        {
            if (smsVM != null)
            {
                var sms = smsTemplateTasks.One(smsVM.SmsTemplateID);
                if (sms != null)
                {
                    sms.ModifiedDate = DateTime.Now;
                    sms.ModifiedBy = User.Identity.Name;
                    sms.DataStatus = "Deleted";
                    smsTemplateTasks.Update(sms);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<SmsTemplateViewModel> GetSmsTemplate()
        {
            var smsTemplates = this.smsTemplateTasks.GetListNotDeleted();

            return from sms in smsTemplates
                   select new SmsTemplateViewModel
        {
            SmsTemplateID = sms.Id,
            WOStatus = sms.WOStatus,
            SmsTemplateText = sms.SmsTemplateText,
            SmsTemplateStatus = sms.SmsTemplateStatus,
            SmsTemplateDesc = sms.SmsTemplateDesc
        };
        }
    }
}
