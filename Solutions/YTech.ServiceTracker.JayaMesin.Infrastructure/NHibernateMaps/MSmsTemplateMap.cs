using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public class MSmsTemplateMap : IAutoMappingOverride<MSmsTemplate>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<MSmsTemplate> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.JAYAMESIN_M_SMS_TEMPLATE");
            mapping.Id(x => x.Id, "[SMS_TEMPLATE_ID]")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.WOStatus, "[WO_STATUS]");
            mapping.Map(x => x.SmsTemplateText, "[SMS_TEMPLATE_TEXT]");

            mapping.Map(x => x.SmsTemplateStatus, "[SMS_TEMPLATE_STATUS]");
            mapping.Map(x => x.SmsTemplateDesc, "[SMS_TEMPLATE_DESC]");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }
    }
}
