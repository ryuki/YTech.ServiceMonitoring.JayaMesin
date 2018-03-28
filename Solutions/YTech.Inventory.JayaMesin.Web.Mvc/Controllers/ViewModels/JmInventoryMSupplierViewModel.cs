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
    public class JmInventoryMSupplierViewModel
    {
        #region Properties
    
        [ScaffoldColumn(false)]
        public string SupplierId
        {
            get;
            set;
        }
        
        
        [DisplayName("Nama Supplier")]
        public virtual string SupplierName { get; set; }

        [DisplayName("Alamat")]
        [UIHint("TextArea")]
        public virtual string SupplierAddress { get; set; }
        
        [DisplayName("No Telp")]
        public virtual string SupplierPhone { get; set; }

        [DisplayName("Status")]
        [UIHint("SupplierStatus")]
        public virtual string SupplierStatus { get; set; }

        [DisplayName("Keterangan")]
        [UIHint("TextArea")]
        public virtual string SupplierDesc { get; set; }

        [DisplayName("NPWP")]
        public virtual string SupplierNpwp { get; set; }

        [DisplayName("Gambar NPWP")]
        [UIHint("FileUpload")]
        public virtual string SupplierNpwpImgUrl { get; set; }

        #endregion
    }
}
