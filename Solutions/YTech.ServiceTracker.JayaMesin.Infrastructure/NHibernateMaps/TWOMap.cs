using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public class TWOMap : IAutoMappingOverride<TWO>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TWO> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.JAYAMESIN_T_WO");
            mapping.Id(x => x.Id, "WO_ID")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.WONo, "WO_NO");
            mapping.References(x => x.CustomerId, "CUSTOMER_ID").Fetch.Join();
            mapping.Map(x => x.WODate, "WO_DATE");
            mapping.Map(x => x.WOItemType, "WO_ITEM_TYPE");
            mapping.Map(x => x.WOItemSn, "WO_ITEM_SN");
            mapping.Map(x => x.WOIsGuarantee, "WO_IS_GUARANTEE");
            mapping.Map(x => x.WOEquipments, "WO_EQUIPMENTS");
            mapping.Map(x => x.WOScStore, "WO_SC_STORE");
            mapping.Map(x => x.WOPriority, "WO_PRIORITY");
            mapping.Map(x => x.WOStartDate, "WO_START_DATE");
            mapping.Map(x => x.WOLastStatus, "WO_LAST_STATUS");
            mapping.Map(x => x.WOEstFinishDate, "WO_EST_FINISH_DATE");
            mapping.Map(x => x.WOTotal, "WO_TOTAL");
            mapping.Map(x => x.WODp, "WO_DP");
            mapping.Map(x => x.WOInvoiceNo, "WO_INVOICE_NO");
            mapping.Map(x => x.WOTakenDate, "WO_TAKEN_DATE");
            mapping.Map(x => x.WOBrokenDesc, "WO_BROKEN_DESC");
            mapping.Map(x => x.WODesc, "WO_DESC");
            mapping.Map(x => x.WOComplain, "WO_COMPLAIN");
            mapping.Map(x => x.WORemarkStatus, "WO_REMARK_STATUS");
            mapping.Map(x => x.WOReceivedBy, "WO_RECEIVED_BY");
            mapping.Map(x => x.WORepairedBy, "WO_REPAIRED_BY");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();


            //mapping.HasOne(x => x.WOLog)
            //    .Class<TWOLog>()
            //    .ForeignKey("WO_ID");

            mapping.HasMany(x => x.WOLogs)
                .AsBag()
                .Inverse()
                .KeyColumn("WO_ID");
        }
    }
}
