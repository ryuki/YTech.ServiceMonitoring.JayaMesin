using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public partial class JmInventoryMProductSupplierMap : IAutoMappingOverride<JmInventoryMProductSupplier>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<JmInventoryMProductSupplier> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[JM_INVENTORY_M_PRODUCT_SUPPLIER]");
			mapping.Id(x => x.Id, "[PRODUCT_SUPPLIER_ID]")
                 .GeneratedBy.Assigned();

            mapping.References<JmInventoryMProduct>(x => x.ProductId, "[PRODUCT_ID]").ForeignKey();
            mapping.References<JmInventoryMSupplier>(x => x.SupplierId, "[SUPPLIER_ID]").ForeignKey();
            
            mapping.Map(x => x.ProductSupplierDesc, "[PRODUCT_SUPPLIER_DESC]");
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
