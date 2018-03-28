using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers.ViewModels
{
    public class SmsTemplateViewModel
    {
        [ScaffoldColumn(false)]
        public string SmsTemplateID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Status WO")]
        [UIHint("WOStatus")]
        public string WOStatus
        {
            get;
            set;
        }

        //[Required]
        [DisplayName("Template SMS")]
        [UIHint("TextArea")]
        public string SmsTemplateText
        {
            get;
            set;
        }

        //[Required]
        [DisplayName("Status")]
        public string SmsTemplateStatus
        {
            get;
            set;
        }

        [DisplayName("Keterangan")]
        [UIHint("TextArea")]
        public string SmsTemplateDesc
        {
            get;
            set;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CustomProductNameValidationAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            var productName = (string)value;
            if (!string.IsNullOrEmpty(productName))
            {
                return Regex.IsMatch(productName, "^[A-Z]");
            }
            return true;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessage,
                ValidationType = "productnamevalidation"
            };
        }
    }
}