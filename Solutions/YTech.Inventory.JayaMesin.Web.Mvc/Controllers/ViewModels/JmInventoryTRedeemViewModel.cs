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
    public class JmInventoryTRedeemViewModel
    {
        #region Properties
    
        [ScaffoldColumn(false)]
        public string RedeemId
        {
            get;
            set;
        }
        
        [DisplayName("ProgramId")]
        [UIHint("JmInventoryMProgram")]
        public string ProgramId { get; set; }
        
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string ProgramIdName { get; set; }
        
        [DisplayName("RedeemType")]
        public virtual string RedeemType { get; set; }
        
        [DisplayName("RedeemDate")]
        public virtual System.DateTime? RedeemDate { get; set; }
        
        [DisplayName("RedeemValueTotal")]
        public virtual decimal? RedeemValueTotal { get; set; }
        
        [DisplayName("RedeemStatus")]
        public virtual string RedeemStatus { get; set; }
        
        [DisplayName("RedeemDateOut")]
        public virtual System.DateTime? RedeemDateOut { get; set; }
        
        [DisplayName("RedeemDateCair")]
        public virtual System.DateTime? RedeemDateCair { get; set; }
        
        [DisplayName("CustomerId")]
        public virtual string CustomerId { get; set; }
        
        [DisplayName("RedeemDesc")]
        public virtual string RedeemDesc { get; set; }
        

        #endregion
    }
}
