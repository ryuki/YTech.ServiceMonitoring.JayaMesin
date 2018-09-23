using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmIntrackTInvoiceLogTasks
    {
       IEnumerable<JmIntrackTInvoiceLog> GetAll();
        JmIntrackTInvoiceLog Insert(JmIntrackTInvoiceLog entity);
        JmIntrackTInvoiceLog Update(JmIntrackTInvoiceLog entity);
        JmIntrackTInvoiceLog Delete(JmIntrackTInvoiceLog entity);
        JmIntrackTInvoiceLog One(string id);
        IEnumerable<JmIntrackTInvoiceLog> GetListNotDeleted();
    }
}
