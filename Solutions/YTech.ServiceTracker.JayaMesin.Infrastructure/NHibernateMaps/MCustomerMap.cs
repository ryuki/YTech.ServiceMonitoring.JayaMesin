using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public class MCustomerMap : IAutoMappingOverride<MCustomer>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<MCustomer> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.M_CUSTOMER");
            mapping.Id(x => x.Id, "CUSTOMER_ID")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.CustomerName, "CUSTOMER_NAME");
            mapping.Map(x => x.CustomerDesc, "CUSTOMER_DESC");

            mapping.Map(x => x.CustomerPhone, "CUSTOMER_PHONE");
            mapping.Map(x => x.CustomerAddress, "CUSTOMER_ADDRESS");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }
    }
}
