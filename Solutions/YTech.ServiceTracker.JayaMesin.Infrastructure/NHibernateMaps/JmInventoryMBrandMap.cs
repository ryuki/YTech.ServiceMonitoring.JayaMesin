using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public partial class JmInventoryMBrandMap : IAutoMappingOverride<JmInventoryMBrand>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<JmInventoryMBrand> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[JM_INVENTORY_M_BRAND]");
			mapping.Id(x => x.Id, "[BRAND_ID]")
                 .GeneratedBy.Assigned();

            
            mapping.Map(x => x.BrandName, "[BRAND_NAME]");
            mapping.Map(x => x.BrandDesc, "[BRAND_DESC]");
            mapping.Map(x => x.BrandImg, "[BRAND_IMG]");
            mapping.Map(x => x.BrandImgUrl, "[BRAND_IMG_URL]");
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
