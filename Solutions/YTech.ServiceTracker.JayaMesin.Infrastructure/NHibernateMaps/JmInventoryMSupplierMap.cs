using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public partial class JmInventoryMSupplierMap : IAutoMappingOverride<JmInventoryMSupplier>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<JmInventoryMSupplier> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[JM_INVENTORY_M_SUPPLIER]");
			mapping.Id(x => x.Id, "[SUPPLIER_ID]")
                 .GeneratedBy.Assigned();

            
            mapping.Map(x => x.SupplierName, "[SUPPLIER_NAME]");
            mapping.Map(x => x.SupplierAddress, "[SUPPLIER_ADDRESS]");
            mapping.Map(x => x.SupplierPhone, "[SUPPLIER_PHONE]");
            mapping.Map(x => x.SupplierStatus, "[SUPPLIER_STATUS]");
            mapping.Map(x => x.SupplierDesc, "[SUPPLIER_DESC]");
            mapping.Map(x => x.DataStatus, "[DATA_STATUS]");
            mapping.Map(x => x.CreatedBy, "[CREATED_BY]");
            mapping.Map(x => x.CreatedDate, "[CREATED_DATE]");
            mapping.Map(x => x.ModifiedBy, "[MODIFIED_BY]");
            mapping.Map(x => x.ModifiedDate, "[MODIFIED_DATE]");
            mapping.Map(x => x.RowVersion, "[ROW_VERSION]");
            mapping.Map(x => x.SupplierNpwp, "[SUPPLIER_NPWP]");
            mapping.Map(x => x.SupplierNpwpImg, "[SUPPLIER_NPWP_IMG]");
            mapping.Map(x => x.SupplierNpwpImgUrl, "[SUPPLIER_NPWP_IMG_URL]");
            mapping.Map(x => x.SupplierDebtLimit, "[SUPPLIER_DEBT_LIMIT]");
            mapping.Map(x => x.SupplierDebtTermin, "[SUPPLIER_DEBT_TERMIN]");

            #endregion
        }
    }
}
