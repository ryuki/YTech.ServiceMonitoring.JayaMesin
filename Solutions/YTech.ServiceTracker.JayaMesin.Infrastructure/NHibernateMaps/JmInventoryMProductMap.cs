using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public partial class JmInventoryMProductMap : IAutoMappingOverride<JmInventoryMProduct>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<JmInventoryMProduct> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.JM_INVENTORY_M_PRODUCT");
			mapping.Id(x => x.Id, "PRODUCT_ID")
                 .GeneratedBy.Assigned();

            mapping.References<JmInventoryMBrand>(x => x.BrandId, "BRAND_ID").ForeignKey();
            mapping.References<JmInventoryMCat>(x => x.CatId, "CAT_ID").ForeignKey();
            
            mapping.Map(x => x.ProductName, "PRODUCT_NAME");
            mapping.Map(x => x.ProductType, "PRODUCT_TYPE");
            mapping.Map(x => x.ProductMerk, "PRODUCT_MERK");
            mapping.Map(x => x.ProductStatus, "PRODUCT_STATUS");
            mapping.Map(x => x.ProductDesc, "PRODUCT_DESC");
            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
            mapping.Map(x => x.ProductImg, "PRODUCT_IMG");
            mapping.Map(x => x.ProductImgUrl, "PRODUCT_IMG_URL");
            mapping.Map(x => x.ProductLastPrice, "PRODUCT_LAST_PRICE");
            mapping.Map(x => x.ProductEstStock, "PRODUCT_EST_STOCK");
            mapping.Map(x => x.ProductEstStockDate, "PRODUCT_EST_STOCK_DATE");
            mapping.Map(x => x.ProductMinStock, "PRODUCT_MIN_STOCK");
            mapping.Map(x => x.ProductPriceSales, "PRODUCT_PRICE_SALES");
            mapping.Map(x => x.ProductLastPriceDate, "PRODUCT_LAST_PRICE_DATE");

            mapping.Map(x => x.HaveBeenRead, "HaveBeenRead").ReadOnly().Not.Update().Not.Insert();
            #endregion
        }
    }
}
