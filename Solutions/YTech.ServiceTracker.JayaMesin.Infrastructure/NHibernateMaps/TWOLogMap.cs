using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public class TWOLogMap : IAutoMappingOverride<TWOLog>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TWOLog> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.JAYAMESIN_T_WO_LOG");
            mapping.Id(x => x.Id, "LOG_ID")
                 .GeneratedBy.Assigned();

            mapping.References(x => x.WOId, "WO_ID").Fetch.Join();
            mapping.Map(x => x.LogUser, "LOG_USER");
            mapping.Map(x => x.LogStatus, "LOG_STATUS");
            mapping.Map(x => x.LogDate, "LOG_DATE");
            mapping.Map(x => x.LogDesc, "LOG_DESC");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }
    }
}
