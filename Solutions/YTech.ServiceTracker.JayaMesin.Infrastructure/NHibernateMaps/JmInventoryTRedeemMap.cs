using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public partial class JmInventoryTRedeemMap : IAutoMappingOverride<JmInventoryTRedeem>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<JmInventoryTRedeem> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[JM_INVENTORY_T_REDEEM]");
			mapping.Id(x => x.Id, "[REDEEM_ID]")
                 .GeneratedBy.Assigned();

            mapping.References<JmInventoryMProgram>(x => x.ProgramId, "[PROGRAM_ID]").ForeignKey();
            
            mapping.Map(x => x.RedeemType, "[REDEEM_TYPE]");
            mapping.Map(x => x.RedeemDate, "[REDEEM_DATE]");
            mapping.Map(x => x.RedeemValueTotal, "[REDEEM_VALUE_TOTAL]");
            mapping.Map(x => x.RedeemStatus, "[REDEEM_STATUS]");
            mapping.Map(x => x.RedeemDateOut, "[REDEEM_DATE_OUT]");
            mapping.Map(x => x.RedeemDateCair, "[REDEEM_DATE_CAIR]");
            mapping.Map(x => x.CustomerId, "[CUSTOMER_ID]");
            mapping.Map(x => x.RedeemDesc, "[REDEEM_DESC]");
            mapping.Map(x => x.DataStatus, "[DATA_STATUS]");
            mapping.Map(x => x.CreatedBy, "[CREATED_BY]");
            mapping.Map(x => x.CreatedDate, "[CREATED_DATE]");
            mapping.Map(x => x.ModifiedBy, "[MODIFIED_BY]");
            mapping.Map(x => x.ModifiedDate, "[MODIFIED_DATE]");
            mapping.Map(x => x.RowVersion, "[ROW_VERSION]");

            #endregion
        }
    }
}
