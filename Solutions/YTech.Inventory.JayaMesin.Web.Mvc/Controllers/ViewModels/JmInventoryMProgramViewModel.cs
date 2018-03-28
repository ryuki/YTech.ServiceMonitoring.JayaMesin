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
    public class JmInventoryMProgramViewModel
    {
        #region Properties

        [ScaffoldColumn(false)]
        public string ProgramId
        {
            get;
            set;
        }


        [DisplayName("Nama Program")]
        public virtual string ProgramName { get; set; }

        [DisplayName("Tanggal Expired")]
        [UIHint("Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public virtual System.DateTime? ProgramExpiredDate { get; set; }

        [DisplayName("Tipe Proram")]
        public virtual string ProgramType { get; set; }

        [DisplayName("Status")]
        [UIHint("ProgramStatus")]
        public virtual string ProgramStatus { get; set; }

        [DisplayName("Keterangan")]
        [UIHint("TextArea")]
        public virtual string ProgramDesc { get; set; }

        [DisplayName("Gambar Program")]
        [UIHint("FileUpload")]
        public virtual string ProgramImgUrl { get; set; }


        #endregion
    }
}
