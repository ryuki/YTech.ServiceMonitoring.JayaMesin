using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public partial class JmInventoryTRedeemDetMap : IAutoMappingOverride<JmInventoryTRedeemDet>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<JmInventoryTRedeemDet> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[JM_INVENTORY_T_REDEEM_DET]");
			mapping.Id(x => x.Id, "[REDEEM_DET_ID]")
                 .GeneratedBy.Assigned();

            mapping.References<JmInventoryTRedeem>(x => x.RedeemId, "[REDEEM_ID]").ForeignKey();
            mapping.References<JmInventoryTStock>(x => x.StockId, "[STOCK_ID]").ForeignKey();
            
            mapping.Map(x => x.RedeemDetValue, "[REDEEM_DET_VALUE]");
            mapping.Map(x => x.RedeemDetStatus, "[REDEEM_DET_STATUS]");
            mapping.Map(x => x.RedeemDetDesc, "[REDEEM_DET_DESC]");
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
