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
        Baru, Ditolak, Disetujui, Diproses
    }

    public enum EnumTransLog
    {
        Save,
        Update,
        Delete,
        Read,
        Print,
        Approve
    }

    public enum EnumLogType
    {
        Trans_PO
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
}
