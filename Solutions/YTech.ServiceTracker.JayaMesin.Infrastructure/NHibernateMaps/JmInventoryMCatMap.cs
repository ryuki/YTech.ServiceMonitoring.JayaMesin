using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public partial class JmInventoryMCatMap : IAutoMappingOverride<JmInventoryMCat>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<JmInventoryMCat> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[JM_INVENTORY_M_CAT]");
			mapping.Id(x => x.Id, "[CAT_ID]")
                 .GeneratedBy.Assigned();

            
            mapping.Map(x => x.CatParentId, "[CAT_PARENT_ID]");
            mapping.Map(x => x.CatName, "[CAT_NAME]");
            mapping.Map(x => x.CatDesc, "[CAT_DESC]");
            mapping.Map(x => x.CatImg, "[CAT_IMG]");
            mapping.Map(x => x.CatImgUrl, "[CAT_IMG_URL]");
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
