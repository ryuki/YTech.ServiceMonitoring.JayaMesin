using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public class TSmsMap : IAutoMappingOverride<TSms>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TSms> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[JAYAMESIN_T_SMS]");
            mapping.Id(x => x.Id, "[SMS_ID]")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.SmsRecipient, "[SMS_RECIPIENT]");
            mapping.Map(x => x.SmsText, "[SMS_TEXT]");

            mapping.Map(x => x.SmsReport, "[SMS_REPORT]");
            mapping.Map(x => x.SmsStatus, "[SMS_STATUS]");
            mapping.Map(x => x.SmsDesc, "[SMS_DESC]");
            mapping.Map(x => x.SmsDate, "[SMS_DATE]");
            mapping.References(x => x.WOId, "WO_ID").Fetch.Join();

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }
    }
}
