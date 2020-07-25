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
        [DisplayName("Part Number")]
        [Required]
        public string ProductId
        {
            get;
            set;
        }        
        
        [DisplayName("Nama Produk")]
        public virtual string ProductName { get; set; }

        [DisplayName("Kategori")]
        [UIHint("Cat")]
        public virtual string CatId { get; set; }

        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string CatName { get; set; }

        [DisplayName("Merek")]
        [UIHint("Brand")]
        public virtual string BrandId { get; set; }

        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string BrandName { get; set; }
        
        [DisplayName("Harga Beli Terakhir (Rp)")]
        public virtual decimal? ProductLastPrice { get; set; }

        [DisplayName("Tgl Beli Terakhir")]
        public virtual System.DateTime? ProductLastPriceDate { get; set; }

        [DisplayName("Estimasi Stok")]
        public virtual decimal? ProductEstStock { get; set; }

        [DisplayName("Tgl Estimasi Stok")]
        public virtual System.DateTime? ProductEstStockDate { get; set; }

        [DisplayName("Minimal Stok")]
        public virtual decimal? ProductMinStock { get; set; }

        [DisplayName("Harga Jual (Rp)")]
        public virtual decimal? ProductPriceSales { get; set; }

        [DisplayName("Status")]
        [UIHint("ProductStatus")]
        public virtual string ProductStatus { get; set; }

        [DisplayName("Deskripsi Produk")]
        [UIHint("TextArea")]
        public virtual string ProductDesc { get; set; }

        [DisplayName("Gambar Produk")]
        [UIHint("FileUpload")]
        public virtual string ProductImgUrl { get; set; }

        public virtual int HaveBeenRead { get; set; }
        #endregion
    }
}
