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
    public class JmInventoryMExpeditionViewModel
    {
        #region Properties

        [ScaffoldColumn(true)]
        [DisplayName("Kode Ekspedisi")]
        [Required]
        public string ExpeditionId
        {
            get;
            set;
        }


        [DisplayName("Nama Ekspedisi")]
        public virtual string ExpeditionName { get; set; }

        [DisplayName("Alamat")]
        [UIHint("TextArea")]        
        public virtual string ExpeditionAddress { get; set; }

        [DisplayName("No HP")]
        public virtual string ExpeditionPhone { get; set; }

        [DisplayName("Keterangan")]
        [UIHint("TextArea")]
        public virtual string ExpeditionDesc { get; set; }
        
        [DisplayName("ExpeditionImg")]
        public virtual byte[] ExpeditionImg { get; set; }

        [DisplayName("Gambar Merek")]
        [UIHint("FileUpload")]
        public virtual string ExpeditionImgUrl { get; set; }

        #endregion
    }
}
