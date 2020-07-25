using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmIntrackTInvoiceTasks
    {
       IEnumerable<JmIntrackTInvoice> GetAll();
        JmIntrackTInvoice Insert(JmIntrackTInvoice entity);
        JmIntrackTInvoice Update(JmIntrackTInvoice entity);
        JmIntrackTInvoice Delete(JmIntrackTInvoice entity);
        JmIntrackTInvoice One(string id);
        IEnumerable<JmIntrackTInvoice> GetListNotDeleted();

        void AutoUpdateInvoiceStatus();

        IEnumerable<JmIntrackTInvoiceHaveRead> GetListHaveBeenRead(string UserName, string TextSearch);
    }
}
