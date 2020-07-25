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
    public class JmIntrackTInvoiceViewModel
    {
        #region Properties

        [ScaffoldColumn(false)]
        public string InvoiceId
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Pilih Lokasi!")]
        [DisplayName("Lokasi")]
        [UIHint("InvoiceLocation")]
        public virtual string LocationId { get; set; }

        [Required(ErrorMessage = "No Faktur harus diisi!")]
        [DisplayName("No Faktur")]
        public virtual string InvoiceNo { get; set; }

        [Required(ErrorMessage = "No Paket harus diisi!")]
        [DisplayName("No Paket")]
        public virtual string InvoicePacketNo { get; set; }

        private System.DateTime? _invoiceDate = null;

        [Required(ErrorMessage = "Tgl Faktur harus diisi!")]
        [DisplayName("Tgl Faktur")]
        public virtual System.DateTime? InvoiceDate { get; set; }
        //{
        //    get { return _invoiceDate; }
        //    set
        //    {
        //        if (value.HasValue)
        //        {
        //            this._invoiceDate = value.Value.ToUniversalTime(); // = new DateTime(value.Value.Ticks, DateTimeKind.Utc);
        //        }
        //        else
        //            this._invoiceDate = null;
        //    }
        //}

        [Required(ErrorMessage = "Nama Pelanggan harus diisi!")]
        [DisplayName("Nama Pelanggan")]
        public virtual string CustomerId { get; set; }

        [Required(ErrorMessage = "Nilai Faktur harus diisi!")]
        [DisplayName("Nilai Faktur (Rp)")]
        public virtual decimal? InvoiceAmmount { get; set; }

        [DisplayName("Sudah Dibayar (Rp)")]
        public virtual decimal? InvoicePaid { get; set; }

        [Required(ErrorMessage = "Admin Paket harus diisi!")]
        [DisplayName("Admin Paket")]
        public virtual string InvoicePacketAdmin { get; set; }

        [Required(ErrorMessage = "Nama Sales harus diisi!")]
        [DisplayName("Nama Sales")]
        public virtual string SalesmanId { get; set; }

        [DisplayName("Umur Faktur")]
        public virtual decimal? InvoiceAge { get; set; }

        [DisplayName("Tgl Jatuh Tempo")]
        public virtual System.DateTime? InvoiceDueDate { get; set; }

        [DisplayName("Pengiriman")]
        public virtual string InvoiceDelivery { get; set; }

        [DisplayName("Tgl Lunas")]
        public virtual System.DateTime? InvoicePaidOffDate { get; set; }

        [DisplayName("Status Faktur")]
        [UIHint("InvoiceStatus")]
        public virtual string InvoiceStatus { get; set; }

        [DisplayName("Keterangan")]
        [UIHint("TextArea")]
        public virtual string InvoiceDesc { get; set; }

        [DisplayName("Surat Pesanan")]
        [UIHint("Checkbox")]
        public bool InvoiceDocLetter { get; set; }

        [DisplayName("SSP PPN")]
        [UIHint("Checkbox")]
        public bool InvoiceDocSspPpn { get; set; }

        [DisplayName("SSP PPH")]
        [UIHint("Checkbox")]
        public bool InvoiceDocSspPph { get; set; }

        [Required(ErrorMessage = "Helpdesk harus diisi!")]
        [DisplayName("Helpdesk")]
        public virtual string InvoiceHelpdesk { get; set; }

        [Required(ErrorMessage = "Sumber Dana harus diisi!")]
        [DisplayName("Sumber Dana")]
        public virtual string InvoiceFundSource { get; set; }

        [Required(ErrorMessage = "Pilih Daerah!")]
        [DisplayName("Daerah")]
        [UIHint("InvoiceLocationType")]
        public virtual string InvoiceLocationType { get; set; }

        [DisplayName("Bank")]
        public virtual string InvoiceBank { get; set; }

        [DisplayName("No Faktur Pajak")]
        public virtual string InvoiceTaxInvoiceNo { get; set; }

        [DisplayName("DO")]
        [UIHint("Checkbox")]
        public bool InvoiceDocDo { get; set; }

        [DisplayName("Invoice")]
        [UIHint("Checkbox")]
        public bool InvoiceDocInvoice { get; set; }

        [DisplayName("SPM")]
        [UIHint("Checkbox")]
        public bool InvoiceDocSpm { get; set; }

        [DisplayName("FC Kwitansi")]
        [UIHint("Checkbox")]
        public bool InvoiceDocReceiptCopy { get; set; }

        [DisplayName("SPK")]
        [UIHint("Checkbox")]
        public bool InvoiceDocSpk { get; set; }

        [DisplayName("BAST")]
        [UIHint("Checkbox")]
        public bool InvoiceDocBast { get; set; }

        [DisplayName("BAPHP")]
        [UIHint("Checkbox")]
        public bool InvoiceDocBaphp { get; set; }

        [DisplayName("BAP")]
        [UIHint("Checkbox")]
        public bool InvoiceDocBap { get; set; }

        [DisplayName("Nilai")]
        public virtual decimal? InvoiceDocSspPpnValue { get; set; }

        [DisplayName("NTPN")]
        public virtual string InvoiceDocSspPpnNtpn { get; set; }

        [DisplayName("Tgl")]
        public virtual System.DateTime? InvoiceDocSspPpnDate { get; set; }

        [DisplayName("Ket")]
        public virtual string InvoiceDocSspPpnDesc { get; set; }

        [DisplayName("Nilai")]
        public virtual decimal? InvoiceDocSspPphValue { get; set; }

        [DisplayName("NTPN")]
        public virtual string InvoiceDocSspPphNtpn { get; set; }

        [DisplayName("Tgl")]
        public virtual System.DateTime? InvoiceDocSspPphDate { get; set; }

        [DisplayName("Ket")]
        public virtual string InvoiceDocSspPphDesc { get; set; }

        public virtual string InvoiceIsUrgent { get; set; }
        public virtual string InvoiceToCopy { get; set; }
        public virtual int HaveBeenRead { get; set; }
        public virtual int InvoiceStatusOrder { get; set; }

        [DisplayName("DPP")]
        public virtual string InvoiceAmmountDPP { get; set; }
        [DisplayName("PPN")]
        public virtual string InvoiceAmmountPPN { get; set; }
        [DisplayName("PPH")]
        public virtual string InvoiceAmmountPPH { get; set; }
        [DisplayName("Kas Masuk")]
        public virtual string InvoiceAmmountCashIn { get; set; }

        #endregion
    }

    public class DashboardInvoiceViewModel
    {
        public virtual string InvoiceStatus { get; set; }
        public virtual decimal? InvoiceTotal { get; set; }
        public virtual string Color { get; set; }
        public virtual string Salesman { get; set; }
        public virtual decimal? InvoiceAmmountTotal { get; set; }
    }
}
