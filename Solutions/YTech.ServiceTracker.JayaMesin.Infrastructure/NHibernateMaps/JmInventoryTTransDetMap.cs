using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public partial class JmInventoryTTransDetMap : IAutoMappingOverride<JmInventoryTTransDet>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<JmInventoryTTransDet> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[JM_INVENTORY_T_TRANS_DET]");
			mapping.Id(x => x.Id, "[TRANS_DET_ID]")
                 .GeneratedBy.Assigned();

            mapping.References<JmInventoryMProduct>(x => x.ProductId, "[PRODUCT_ID]").ForeignKey();
            mapping.References<JmInventoryTTran>(x => x.TransId, "[TRANS_ID]").ForeignKey();
            
            mapping.Map(x => x.TransDetQty, "[TRANS_DET_QTY]");
            mapping.Map(x => x.TransDetStatus, "[TRANS_DET_STATUS]");
            mapping.Map(x => x.TransDetDesc, "[TRANS_DET_DESC]");
            mapping.Map(x => x.DataStatus, "[DATA_STATUS]");
            mapping.Map(x => x.CreatedBy, "[CREATED_BY]");
            mapping.Map(x => x.CreatedDate, "[CREATED_DATE]");
            mapping.Map(x => x.ModifiedBy, "[MODIFIED_BY]");
            mapping.Map(x => x.ModifiedDate, "[MODIFIED_DATE]");
            mapping.Map(x => x.RowVersion, "[ROW_VERSION]");
            mapping.Map(x => x.TransDetPrice, "[TRANS_DET_PRICE]");
            mapping.Map(x => x.TransDetDisc, "[TRANS_DET_DISC]");
            mapping.Map(x => x.TransDetIsTaxed, "[TRANS_DET_IS_TAXED]");
            mapping.Map(x => x.TransDetTotal, "[TRANS_DET_TOTAL]");
            mapping.Map(x => x.TransDetOngkir, "[TRANS_DET_ONGKIR]");

            #endregion
        }
    }
}
