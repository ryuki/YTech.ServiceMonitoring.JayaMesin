using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public class TWOStatusMap : IAutoMappingOverride<TWOStatus>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TWOStatus> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.T_WO_STATUS");
            mapping.Id(x => x.Id, "WO_STATUS_ID")
                 .GeneratedBy.Assigned();

            mapping.References(x => x.WOId, "WO_ID").Fetch.Join();
            mapping.Map(x => x.WOStatus, "WO_STATUS");
            mapping.Map(x => x.WOStatusUser, "WO_STATUS_USER");
            mapping.Map(x => x.WOStatusDate, "WO_STATUS_DATE");
            mapping.Map(x => x.WOStatusBrokenDesc, "WO_STATUS_BROKEN_DESC");
            mapping.Map(x => x.WOStatusStartDate, "WO_STATUS_START_DATE");
            mapping.Map(x => x.WOStatusFinishDate, "WO_STATUS_FINISH_DATE");
            mapping.Map(x => x.WOStatusDesc, "WO_STATUS_DESC");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }
    }
}
