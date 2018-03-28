using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public partial class JmInventoryTTranMap : IAutoMappingOverride<JmInventoryTTran>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<JmInventoryTTran> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[JM_INVENTORY_T_TRANS]");
			mapping.Id(x => x.Id, "[TRANS_ID]")
                 .GeneratedBy.Assigned();

            mapping.References<JmInventoryTTran>(x => x.TransRefId, "[TRANS_REF_ID]").ForeignKey();
            mapping.References<JmInventoryMSupplier>(x => x.SupplierId, "[SUPPLIER_ID]").ForeignKey();
            
            mapping.Map(x => x.TransNo, "[TRANS_NO]");
            mapping.Map(x => x.TransDate, "[TRANS_DATE]");
            mapping.Map(x => x.TransType, "[TRANS_TYPE]");
            mapping.Map(x => x.TransStatus, "[TRANS_STATUS]");
            mapping.Map(x => x.TransApprovedBy, "[TRANS_APPROVED_BY]");
            mapping.Map(x => x.TransApprovedDate, "[TRANS_APPROVED_DATE]");
            mapping.Map(x => x.TransInputBy, "[TRANS_INPUT_BY]");
            mapping.Map(x => x.TransInputDate, "[TRANS_INPUT_DATE]");
            mapping.Map(x => x.InvoiceNo, "[INVOICE_NO]");
            mapping.Map(x => x.TransDesc, "[TRANS_DESC]");
            mapping.Map(x => x.DataStatus, "[DATA_STATUS]");
            mapping.Map(x => x.CreatedBy, "[CREATED_BY]");
            mapping.Map(x => x.CreatedDate, "[CREATED_DATE]");
            mapping.Map(x => x.ModifiedBy, "[MODIFIED_BY]");
            mapping.Map(x => x.ModifiedDate, "[MODIFIED_DATE]");
            mapping.Map(x => x.RowVersion, "[ROW_VERSION]");
            mapping.Map(x => x.TransSubTotal, "[TRANS_SUB_TOTAL]");
            mapping.Map(x => x.TransDisc, "[TRANS_DISC]");
            mapping.Map(x => x.TransPpn, "[TRANS_PPN]");
            mapping.Map(x => x.TransCost, "[TRANS_COST]");
            mapping.Map(x => x.TransGrandTotal, "[TRANS_GRAND_TOTAL]");

            #endregion
        }
    }
}
