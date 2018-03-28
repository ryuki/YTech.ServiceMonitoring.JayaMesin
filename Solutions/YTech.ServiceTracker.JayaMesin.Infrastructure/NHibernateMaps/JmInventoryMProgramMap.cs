using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public partial class JmInventoryMProgramMap : IAutoMappingOverride<JmInventoryMProgram>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<JmInventoryMProgram> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[JM_INVENTORY_M_PROGRAM]");
			mapping.Id(x => x.Id, "[PROGRAM_ID]")
                 .GeneratedBy.Assigned();

            
            mapping.Map(x => x.ProgramName, "[PROGRAM_NAME]");
            mapping.Map(x => x.ProgramStatus, "[PROGRAM_STATUS]");
            mapping.Map(x => x.ProgramExpiredDate, "[PROGRAM_EXPIRED_DATE]");
            mapping.Map(x => x.ProgramType, "[PROGRAM_TYPE]");
            mapping.Map(x => x.ProgramDesc, "[PROGRAM_DESC]");
            mapping.Map(x => x.DataStatus, "[DATA_STATUS]");
            mapping.Map(x => x.CreatedBy, "[CREATED_BY]");
            mapping.Map(x => x.CreatedDate, "[CREATED_DATE]");
            mapping.Map(x => x.ModifiedBy, "[MODIFIED_BY]");
            mapping.Map(x => x.ModifiedDate, "[MODIFIED_DATE]");
            mapping.Map(x => x.RowVersion, "[ROW_VERSION]");
            mapping.Map(x => x.ProgramImg, "[PROGRAM_IMG]");
            mapping.Map(x => x.ProgramImgUrl, "[PROGRAM_IMG_URL]");

            #endregion
        }
    }
}
