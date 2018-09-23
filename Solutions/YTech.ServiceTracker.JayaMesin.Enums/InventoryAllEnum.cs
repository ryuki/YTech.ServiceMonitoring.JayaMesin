using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Enums
{
    public enum EnumTransType
    {
        PO, Receipt, Sales
    }

    public enum EnumTransStatus
    {
        Baru, Ditolak, Disetujui, Diproses, Direvisi
    }

    public enum EnumTransLog
    {
        Save,
        Update,
        Delete,
        Read,
        Print,
        Approve,
        Open
    }

    public enum EnumLogType
    {
        Trans_PO, Invoice
    }

    public enum EnumInventoryReferenceType
    {
        PONo
    }

    public class UploadFolder
    {
        public static string Supplier = "~/Upload/JmInventoryMSupplier/";
        public static string Program = "~/Upload/JmInventoryMProgram/";
        public static string Product = "~/Upload/JmInventoryMProduct/";
        public static string Cat = "~/Upload/JmInventoryMProduct/";
        public static string Brand = "~/Upload/JmInventoryMProduct/";
    }

    public enum EnumProductStatus
    {
        Aktif, Discontinued
    }

    public enum EnumSupplierStatus
    {
        Aktif, Non_Aktif
    }

    public enum EnumProgramStatus
    {
        Aktif, Non_Aktif
    }

    public enum EnumPriceStatus
    {
        PriceList, PO
    }

    public enum EnumInvReports
    {
        RptProductPriceBySupplier
    }

    public enum EnumInvoiceStatus
    {
        Baru, Dokumen, Invoicing, Baru_Dilunasi, Lunas_BelumLengkap, Lunas_Lengkap
    }

    public enum EnumDocStatus
    {
        Tidak_Ada, Belum_Lengkap, Kurang_Bayar, Lengkap
    }
}
