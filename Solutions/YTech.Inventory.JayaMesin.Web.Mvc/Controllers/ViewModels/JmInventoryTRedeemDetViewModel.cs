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
    public class JmInventoryTRedeemDetViewModel
    {
        #region Properties
    
        [ScaffoldColumn(false)]
        public string RedeemDetId
        {
            get;
            set;
        }
        
        [DisplayName("RedeemId")]
        [UIHint("JmInventoryTRedeem")]
        public string RedeemId { get; set; }
        
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string RedeemIdName { get; set; }
        [DisplayName("StockId")]
        [UIHint("JmInventoryTStock")]
        public string StockId { get; set; }
        
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string StockIdName { get; set; }
        
        [DisplayName("RedeemDetValue")]
        public virtual decimal? RedeemDetValue { get; set; }
        
        [DisplayName("RedeemDetStatus")]
        public virtual string RedeemDetStatus { get; set; }
        
        [DisplayName("RedeemDetDesc")]
        public virtual string RedeemDetDesc { get; set; }
        

        #endregion
    }
}
