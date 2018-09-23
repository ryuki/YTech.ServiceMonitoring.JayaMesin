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
    public class JmInventoryTProductPriceViewModel
    {
        #region Properties
    
        [ScaffoldColumn(false)]
        public string ProductPriceId
        {
            get;
            set;
        }
        
        //[DisplayName("Produk")]
        //[UIHint("JmInventoryMProduct")]
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string ProductId { get; set; }
        
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string ProductIdName { get; set; }

        [DisplayName("Supplier")]
        [UIHint("Supplier")]
        public string SupplierId { get; set; }
        
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string SupplierIdName { get; set; }

        [DisplayName("Tanggal")]
        public virtual System.DateTime? ProductPriceDate { get; set; }

        [DisplayName("Harga")]
        public virtual decimal? ProductPrice { get; set; }

        [DisplayName("Qty")]
        public virtual decimal? ProductPriceQty { get; set; }

        [DisplayName("Status Harga")]
        [UIHint("PriceStatus")]
        public virtual string ProductPriceStatus { get; set; }

        [DisplayName("Ongkos Kirim")]
        public virtual decimal? ProductPriceOngkir { get; set; }
        
        [DisplayName("Keterangan")]
        public virtual string ProductPriceDesc { get; set; }

        [DisplayName("Foto Harga")]
        [UIHint("FileUploadPrice")]
        public virtual string ProductPriceImgUrl { get; set; }

        #endregion
    }
}
