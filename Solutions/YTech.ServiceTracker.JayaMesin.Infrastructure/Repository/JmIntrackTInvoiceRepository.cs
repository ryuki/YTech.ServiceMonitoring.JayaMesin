using NHibernate;
using NHibernate.Criterion;
using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.Repository
{
     public class JmIntrackTInvoiceRepository : NHibernateRepositoryWithTypedId<JmIntrackTInvoice, string>, IJmIntrackTInvoiceRepository
    {
       public IEnumerable<JmIntrackTInvoice> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmIntrackTInvoice));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<JmIntrackTInvoice>();
       }

       public IEnumerable<JmIntrackTInvoiceHaveRead> GetListHaveBeenRead(string UserName, string TextSearch)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine(@"   EXEC [dbo].[JM_INVENTORY_SP_INVOICE_READ]
                        		@User_Name = :UserName, @TextSearch = :TextSearch ");

//           sql.AppendLine(@"   SELECT p.INVOICE_ID
//      ,[INVOICE_NO]
//      ,[INVOICE_PACKET_NO]
//      ,[INVOICE_DATE]
//      ,[CUSTOMER_ID]
//      ,[INVOICE_AMMOUNT]
//      ,[INVOICE_PAID]
//      ,[INVOICE_PACKET_ADMIN]
//      ,[SALESMAN_ID]
//      ,[INVOICE_AGE]
//      ,[INVOICE_DUE_DATE]
//      ,[INVOICE_DELIVERY]
//      ,[INVOICE_PAID_OFF_DATE]
//      ,[INVOICE_STATUS]
//      ,[INVOICE_DESC]
//      ,[INVOICE_DOC_LETTER]
//      ,[INVOICE_DOC_SSP_PPN]
//      ,[INVOICE_DOC_SSP_PPH]
//      ,p.[DATA_STATUS]
//      ,p.[CREATED_BY]
//      ,p.[CREATED_DATE]
//      ,p.[MODIFIED_BY]
//      ,p.[MODIFIED_DATE]
//      ,p.[ROW_VERSION]
//      ,[INVOICE_HELPDESK]
//      ,[INVOICE_FUND_SOURCE]
//      ,[LOCATION_ID]
//      ,[INVOICE_LOCATION_TYPE]
//      ,[INVOICE_BANK]
//      ,[INVOICE_TAX_INVOICE_NO]
//      ,[INVOICE_DOC_DO]
//      ,[INVOICE_DOC_INVOICE]
//      ,[INVOICE_DOC_SPM]
//      ,[INVOICE_DOC_RECEIPT_COPY]
//      ,[INVOICE_DOC_SPK]
//      ,[INVOICE_DOC_BAST]
//      ,[INVOICE_DOC_BAPHP]
//      ,[INVOICE_DOC_BAP]
//      ,[INVOICE_DOC_SSP_PPN_VALUE]
//      ,[INVOICE_DOC_SSP_PPN_NTPN]
//      ,[INVOICE_DOC_SSP_PPN_DATE]
//      ,[INVOICE_DOC_SSP_PPN_DESC]
//      ,[INVOICE_DOC_SSP_PPH_VALUE]
//      ,[INVOICE_DOC_SSP_PPH_NTPN]
//      ,[INVOICE_DOC_SSP_PPH_DATE]
//      ,[INVOICE_DOC_SSP_PPH_DESC],
//  
//case 
//when a.LOG_USER = :UserName then 'true' 
//when a.log_date < b.log_date then 'true'
//else 'false' end HaveBeenRead
//FROM [dbo].[JM_INTRACK_T_INVOICE] p
//LEFT JOIN (select l.*, dense_rank() over (partition by LOG_REF_ID order by row_version desc) ranking
//from JM_INVENTORY_T_LOG l
//where l.log_status <> 'Read' and l.LOG_TYPE = 'Invoice'
//) a
//ON p.[INVOICE_ID] = a.LOG_REF_ID
//and a.ranking = 1
//
//LEFT JOIN (select l.*, dense_rank() over (partition by LOG_REF_ID order by row_version desc) ranking
//from JM_INVENTORY_T_LOG l
//where l.log_status = 'Read' and l.LOG_TYPE = 'Invoice'
//and l.log_user = :UserName
//) b
//ON p.[INVOICE_ID] = b.LOG_REF_ID
//and b.ranking = 1
//
//where p.data_status <> 'Deleted' ");

           IQuery q = Session.CreateSQLQuery(sql.ToString()).AddEntity(typeof(JmIntrackTInvoiceHaveRead));
           q.SetString("UserName", UserName);

           q.SetString("TextSearch", TextSearch);

           return q.List<JmIntrackTInvoiceHaveRead>();
       }

       public void AutoUpdateInvoiceStatus()
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine(@"   update [dbo].[JM_INTRACK_T_INVOICE]
set [INVOICE_STATUS] = 'Lunas_BelumLengkap'
where [INVOICE_STATUS] = 'Baru_Dilunasi' ");
           IQuery q = Session.CreateSQLQuery(sql.ToString());
           q.ExecuteUpdate();
       }
    }
}
