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
    public class TransReceiptDetViewModel
    {
        #region Properties
    
        [ScaffoldColumn(false)]
        public string TransDetId
        {
            get;
            set;
        }

        [DisplayName("Produk")]
        [UIHint("Products")]
        public string ProductId { get; set; }
        
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string ProductName { get; set; }
                
        [DisplayName("Jumlah Produk")]
        public virtual decimal? TransDetQty { get; set; }
        
        [DisplayName("Status")]
        public virtual string TransDetStatus { get; set; }

        [DisplayName("Keterangan")]
        [UIHint("TextArea")]
        public virtual string TransDetDesc { get; set; }
        

        #endregion
    }
}
