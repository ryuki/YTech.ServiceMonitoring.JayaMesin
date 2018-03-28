using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Enums
{
    public enum EnumReferenceType
    {
        WONo
    }

    public enum EnumWOStatus
    {
        Baru_Masuk,
        Sedang_Dikerjakan,
        Menunggu_Persetujuan,
        Sudah_Setuju,
        Menunggu_Part,
        Selesai,
        Batal,
        Closed
    }

    public enum EnumSCToko
    {
        Toko,
        SC
    }

    public enum EnumPriority
    {
        Normal,
        Urgent
    }

    public enum EnumWOType
    {
        Service, Ganti_Sparepart, Service_Dan_Ganti_Sparepart, Klaim_Garansi, Kirim_Ke_Service_Center
    }

    public enum EnumDataStatus
    {
        New,
        Updated, Deleted
    }
}
