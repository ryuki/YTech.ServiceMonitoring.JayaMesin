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
    public class JmInventoryMCatViewModel
    {
        #region Properties

        [ScaffoldColumn(true)]
        [DisplayName("Kode Kategori")]
        [Required]
        public string CatId
        {
            get;
            set;
        }
        
        
        [DisplayName(" ")]
        [HiddenInput]
        public virtual string CatParentId { get; set; }
        
        [DisplayName("Nama Kategori")]
        public virtual string CatName { get; set; }

        [DisplayName("Keterangan")]
        [UIHint("TextArea")]
        public virtual string CatDesc { get; set; }
        
        [DisplayName("CatImg")]
        public virtual byte[] CatImg { get; set; }

        [DisplayName("Gambar Kategori")]
        [UIHint("FileUpload")]
        public virtual string CatImgUrl { get; set; }
        

        #endregion
    }
}
