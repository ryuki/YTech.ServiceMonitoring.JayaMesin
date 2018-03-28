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
    public class CustomerViewModel
    {
        [ScaffoldColumn(false)]
        public string CustomerID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Nama Konsumen")]
        public string CustomerName
        {
            get;
            set;
        }

        //[Required]
        [DisplayName("Telp Konsumen")]
        public string CustomerPhone
        {
            get;
            set;
        }

        //[Required]
        [DisplayName("Alamat Konsumen")]
        [UIHint("TextArea")]
        public string CustomerAddress
        {
            get;
            set;
        }
    }
}