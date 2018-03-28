using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public partial class JmInventoryTStockMap : IAutoMappingOverride<JmInventoryTStock>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<JmInventoryTStock> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[JM_INVENTORY_T_STOCK]");
			mapping.Id(x => x.Id, "[STOCK_ID]")
                 .GeneratedBy.Assigned();

            mapping.References<JmInventoryMProduct>(x => x.ProductId, "[PRODUCT_ID]").ForeignKey();
            
            mapping.Map(x => x.StockSn, "[STOCK_SN]");
            mapping.Map(x => x.StockInTransId, "[STOCK_IN_TRANS_ID]");
            mapping.Map(x => x.StockInDate, "[STOCK_IN_DATE]");
            mapping.Map(x => x.StockOutTransId, "[STOCK_OUT_TRANS_ID]");
            mapping.Map(x => x.StockOutDate, "[STOCK_OUT_DATE]");
            mapping.Map(x => x.StockStatus, "[STOCK_STATUS]");
            mapping.Map(x => x.StockLocation, "[STOCK_LOCATION]");
            mapping.Map(x => x.StockDesc, "[STOCK_DESC]");
            mapping.Map(x => x.DataStatus, "[DATA_STATUS]");
            mapping.Map(x => x.CreatedBy, "[CREATED_BY]");
            mapping.Map(x => x.CreatedDate, "[CREATED_DATE]");
            mapping.Map(x => x.ModifiedBy, "[MODIFIED_BY]");
            mapping.Map(x => x.ModifiedDate, "[MODIFIED_DATE]");
            mapping.Map(x => x.RowVersion, "[ROW_VERSION]");

            mapping.Map(x => x.StockOrderNo, "[STOCK_ORDER_NO]");
            
            #endregion
        }
    }
}
