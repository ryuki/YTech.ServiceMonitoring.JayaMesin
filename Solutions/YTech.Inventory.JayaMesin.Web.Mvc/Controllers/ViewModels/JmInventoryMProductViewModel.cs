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
    public class JmInventoryMProductViewModel
    {
        #region Properties

        [ScaffoldColumn(true)]
        [DisplayName("Kode Produk")]
        [Required]
        public string ProductId
        {
            get;
            set;
        }        
        
        [DisplayName("Nama Produk")]
        public virtual string ProductName { get; set; }
        
        [DisplayName("Tipe")]
        public virtual string ProductType { get; set; }
        
        [DisplayName("Merek")]
        public virtual string ProductMerk { get; set; }

        [DisplayName("Status")]
        [UIHint("ProductStatus")]
        public virtual string ProductStatus { get; set; }

        [DisplayName("Deskripsi Produk")]
        [UIHint("TextArea")]
        public virtual string ProductDesc { get; set; }

        [DisplayName("Gambar Produk")]
        [UIHint("FileUpload")]
        public virtual string ProductImgUrl { get; set; }
        

        #endregion
    }
}
