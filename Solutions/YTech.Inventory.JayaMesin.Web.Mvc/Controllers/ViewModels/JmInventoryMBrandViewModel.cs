using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace YTech.Inventory.JayaMesin.Web.Mvc.Controllers.ViewModels
{
    public class JmInventoryMBrandViewModel
    {
        #region Properties

        [ScaffoldColumn(true)]
        [DisplayName("Kode Merek")]
        [Required]
        public string BrandId
        {
            get;
            set;
        }
        
        
        [DisplayName("Nama Merek")]
        public virtual string BrandName { get; set; }

        [DisplayName("Keterangan")]
        [UIHint("TextArea")]
        public virtual string BrandDesc { get; set; }
        
        [DisplayName("BrandImg")]
        public virtual byte[] BrandImg { get; set; }

        [DisplayName("Gambar Merek")]
        [UIHint("FileUpload")]
        public virtual string BrandImgUrl { get; set; }
        

        #endregion
    }
}
