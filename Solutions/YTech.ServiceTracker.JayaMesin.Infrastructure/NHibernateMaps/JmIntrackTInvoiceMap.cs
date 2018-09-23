using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public partial class JmIntrackTInvoiceMap : IAutoMappingOverride<JmIntrackTInvoice>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<JmIntrackTInvoice> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[JM_INTRACK_T_INVOICE]");
			mapping.Id(x => x.Id, "[INVOICE_ID]")
                 .GeneratedBy.Assigned();

            
            mapping.Map(x => x.InvoiceNo, "[INVOICE_NO]");
            mapping.Map(x => x.InvoicePacketNo, "[INVOICE_PACKET_NO]");
            mapping.Map(x => x.InvoiceDate, "[INVOICE_DATE]");
            mapping.Map(x => x.CustomerId, "[CUSTOMER_ID]");
            mapping.Map(x => x.InvoiceAmmount, "[INVOICE_AMMOUNT]");
            mapping.Map(x => x.InvoicePaid, "[INVOICE_PAID]");
            mapping.Map(x => x.InvoicePacketAdmin, "[INVOICE_PACKET_ADMIN]");
            mapping.Map(x => x.SalesmanId, "[SALESMAN_ID]");
            mapping.Map(x => x.InvoiceAge, "[INVOICE_AGE]");
            mapping.Map(x => x.InvoiceDueDate, "[INVOICE_DUE_DATE]");
            mapping.Map(x => x.InvoiceDelivery, "[INVOICE_DELIVERY]");
            mapping.Map(x => x.InvoicePaidOffDate, "[INVOICE_PAID_OFF_DATE]");
            mapping.Map(x => x.InvoiceStatus, "[INVOICE_STATUS]");
            mapping.Map(x => x.InvoiceDesc, "[INVOICE_DESC]");
            mapping.Map(x => x.InvoiceDocLetter, "[INVOICE_DOC_LETTER]");
            mapping.Map(x => x.InvoiceDocSspPpn, "[INVOICE_DOC_SSP_PPN]");
            mapping.Map(x => x.InvoiceDocSspPph, "[INVOICE_DOC_SSP_PPH]");
            mapping.Map(x => x.DataStatus, "[DATA_STATUS]");
            mapping.Map(x => x.CreatedBy, "[CREATED_BY]");
            mapping.Map(x => x.CreatedDate, "[CREATED_DATE]");
            mapping.Map(x => x.ModifiedBy, "[MODIFIED_BY]");
            mapping.Map(x => x.ModifiedDate, "[MODIFIED_DATE]");
            mapping.Map(x => x.RowVersion, "[ROW_VERSION]");
            mapping.Map(x => x.InvoiceHelpdesk, "[INVOICE_HELPDESK]");
            mapping.Map(x => x.InvoiceFundSource, "[INVOICE_FUND_SOURCE]");

            #endregion
        }
    }
}
