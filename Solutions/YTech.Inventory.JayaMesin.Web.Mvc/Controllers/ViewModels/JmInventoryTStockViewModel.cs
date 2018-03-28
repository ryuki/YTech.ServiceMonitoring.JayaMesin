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
    public class JmInventoryTStockViewModel
    {
        #region Properties
    
        [ScaffoldColumn(false)]
        public string StockId
        {
            get;
            set;
        }
        
        [DisplayName("ProductId")]
        [UIHint("JmInventoryMProduct")]
        public string ProductId { get; set; }
        
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string ProductIdName { get; set; }
        
        [DisplayName("StockSn")]
        public virtual string StockSn { get; set; }
        
        [DisplayName("StockInTransId")]
        public virtual string StockInTransId { get; set; }
        
        [DisplayName("StockInDate")]
        public virtual System.DateTime? StockInDate { get; set; }
        
        [DisplayName("StockOutTransId")]
        public virtual string StockOutTransId { get; set; }
        
        [DisplayName("StockOutDate")]
        public virtual System.DateTime? StockOutDate { get; set; }
        
        [DisplayName("StockStatus")]
        public virtual string StockStatus { get; set; }
        
        [DisplayName("StockLocation")]
        public virtual string StockLocation { get; set; }
        
        [DisplayName("StockDesc")]
        public virtual string StockDesc { get; set; }


        [DisplayName("StockOrderNo")]
        public virtual int? StockOrderNo { get; set; }

        #endregion
    }
}
