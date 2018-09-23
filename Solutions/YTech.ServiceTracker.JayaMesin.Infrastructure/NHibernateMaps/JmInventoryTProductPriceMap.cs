using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public partial class JmInventoryTProductPriceMap : IAutoMappingOverride<JmInventoryTProductPrice>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<JmInventoryTProductPrice> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[JM_INVENTORY_T_PRODUCT_PRICE]");
			mapping.Id(x => x.Id, "[PRODUCT_PRICE_ID]")
                 .GeneratedBy.Assigned();

            mapping.References<JmInventoryMProduct>(x => x.ProductId, "[PRODUCT_ID]").ForeignKey();
            mapping.References<JmInventoryMSupplier>(x => x.SupplierId, "[SUPPLIER_ID]").ForeignKey();
            
            mapping.Map(x => x.ProductPrice, "[PRODUCT_PRICE]");
            mapping.Map(x => x.ProductPriceStatus, "[PRODUCT_PRICE_STATUS]");
            mapping.Map(x => x.ProductPriceDesc, "[PRODUCT_PRICE_DESC]");
            mapping.Map(x => x.DataStatus, "[DATA_STATUS]");
            mapping.Map(x => x.CreatedBy, "[CREATED_BY]");
            mapping.Map(x => x.CreatedDate, "[CREATED_DATE]");
            mapping.Map(x => x.ModifiedBy, "[MODIFIED_BY]");
            mapping.Map(x => x.ModifiedDate, "[MODIFIED_DATE]");
            mapping.Map(x => x.RowVersion, "[ROW_VERSION]");
            mapping.Map(x => x.ProductPriceDate, "[PRODUCT_PRICE_DATE]");
            mapping.Map(x => x.ProductPriceImg, "[PRODUCT_PRICE_IMG]");
            mapping.Map(x => x.ProductPriceImgUrl, "[PRODUCT_PRICE_IMG_URL]");
            mapping.Map(x => x.ProductPriceQty, "[PRODUCT_PRICE_QTY]");
            mapping.Map(x => x.ProductPriceOngkir, "[PRODUCT_PRICE_ONGKIR]");

            #endregion
        }
    }
}
