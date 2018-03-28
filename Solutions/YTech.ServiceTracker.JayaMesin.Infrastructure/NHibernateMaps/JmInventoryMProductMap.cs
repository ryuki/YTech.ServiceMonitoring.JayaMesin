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

            mapping.Table("[dbo].[JM_INVENTORY_M_PRODUCT]");
			mapping.Id(x => x.Id, "[PRODUCT_ID]")
                 .GeneratedBy.Assigned();

            
            mapping.Map(x => x.ProductName, "[PRODUCT_NAME]");
            mapping.Map(x => x.ProductType, "[PRODUCT_TYPE]");
            mapping.Map(x => x.ProductMerk, "[PRODUCT_MERK]");
            mapping.Map(x => x.ProductStatus, "[PRODUCT_STATUS]");
            mapping.Map(x => x.ProductDesc, "[PRODUCT_DESC]");
            mapping.Map(x => x.DataStatus, "[DATA_STATUS]");
            mapping.Map(x => x.CreatedBy, "[CREATED_BY]");
            mapping.Map(x => x.CreatedDate, "[CREATED_DATE]");
            mapping.Map(x => x.ModifiedBy, "[MODIFIED_BY]");
            mapping.Map(x => x.ModifiedDate, "[MODIFIED_DATE]");
            mapping.Map(x => x.RowVersion, "[ROW_VERSION]");
            mapping.Map(x => x.ProductImg, "[PRODUCT_IMG]");
            mapping.Map(x => x.ProductImgUrl, "[PRODUCT_IMG_URL]");

            #endregion
        }
    }
}
