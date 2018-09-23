using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public partial class JmInventoryMExpeditionMap : IAutoMappingOverride<JmInventoryMExpedition>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<JmInventoryMExpedition> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[JM_INVENTORY_M_EXPEDITION]");
			mapping.Id(x => x.Id, "[EXPEDITION_ID]")
                 .GeneratedBy.Assigned();

            
            mapping.Map(x => x.ExpeditionName, "[EXPEDITION_NAME]");
            mapping.Map(x => x.ExpeditionDesc, "[EXPEDITION_DESC]");
            mapping.Map(x => x.ExpeditionImg, "[EXPEDITION_IMG]");
            mapping.Map(x => x.ExpeditionImgUrl, "[EXPEDITION_IMG_URL]");
            mapping.Map(x => x.DataStatus, "[DATA_STATUS]");
            mapping.Map(x => x.CreatedBy, "[CREATED_BY]");
            mapping.Map(x => x.CreatedDate, "[CREATED_DATE]");
            mapping.Map(x => x.ModifiedBy, "[MODIFIED_BY]");
            mapping.Map(x => x.ModifiedDate, "[MODIFIED_DATE]");
            mapping.Map(x => x.RowVersion, "[ROW_VERSION]");
            mapping.Map(x => x.ExpeditionAddress, "[EXPEDITION_ADDRESS]");
            mapping.Map(x => x.ExpeditionPhone, "[EXPEDITION_PHONE]");

            #endregion
        }
    }
}
