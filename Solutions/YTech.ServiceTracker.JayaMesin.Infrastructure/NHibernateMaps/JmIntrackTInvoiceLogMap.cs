using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.NHibernateMaps
{
    public partial class JmIntrackTInvoiceLogMap : IAutoMappingOverride<JmIntrackTInvoiceLog>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<JmIntrackTInvoiceLog> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[JM_INTRACK_T_INVOICE_LOG]");
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
            mapping.Map(x => x.RowVersion, "[ROW_VERSION]").ReadOnly();
            mapping.Map(x => x.InvoiceHelpdesk, "[INVOICE_HELPDESK]");
            mapping.Map(x => x.InvoiceFundSource, "[INVOICE_FUND_SOURCE]");
            mapping.Map(x => x.LocationId, "[LOCATION_ID]");
            mapping.Map(x => x.InvoiceLocationType, "[INVOICE_LOCATION_TYPE]");
            mapping.Map(x => x.InvoiceBank, "[INVOICE_BANK]");
            mapping.Map(x => x.InvoiceTaxInvoiceNo, "[INVOICE_TAX_INVOICE_NO]");
            mapping.Map(x => x.InvoiceDocDo, "[INVOICE_DOC_DO]");
            mapping.Map(x => x.InvoiceDocInvoice, "[INVOICE_DOC_INVOICE]");
            mapping.Map(x => x.InvoiceDocSpm, "[INVOICE_DOC_SPM]");
            mapping.Map(x => x.InvoiceDocReceiptCopy, "[INVOICE_DOC_RECEIPT_COPY]");
            mapping.Map(x => x.InvoiceDocSpk, "[INVOICE_DOC_SPK]");
            mapping.Map(x => x.InvoiceDocBast, "[INVOICE_DOC_BAST]");
            mapping.Map(x => x.InvoiceDocBaphp, "[INVOICE_DOC_BAPHP]");
            mapping.Map(x => x.InvoiceDocBap, "[INVOICE_DOC_BAP]");
            mapping.Map(x => x.InvoiceDocSspPpnValue, "[INVOICE_DOC_SSP_PPN_VALUE]");
            mapping.Map(x => x.InvoiceDocSspPpnNtpn, "[INVOICE_DOC_SSP_PPN_NTPN]");
            mapping.Map(x => x.InvoiceDocSspPpnDate, "[INVOICE_DOC_SSP_PPN_DATE]");
            mapping.Map(x => x.InvoiceDocSspPpnDesc, "[INVOICE_DOC_SSP_PPN_DESC]");
            mapping.Map(x => x.InvoiceDocSspPphValue, "[INVOICE_DOC_SSP_PPH_VALUE]");
            mapping.Map(x => x.InvoiceDocSspPphNtpn, "[INVOICE_DOC_SSP_PPH_NTPN]");
            mapping.Map(x => x.InvoiceDocSspPphDate, "[INVOICE_DOC_SSP_PPH_DATE]");
            mapping.Map(x => x.InvoiceDocSspPphDesc, "[INVOICE_DOC_SSP_PPH_DESC]");

            #endregion
        }
    }
}
