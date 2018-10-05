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
