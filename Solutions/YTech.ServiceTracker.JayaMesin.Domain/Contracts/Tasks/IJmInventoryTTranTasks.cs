using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryTTranTasks
    {
       IEnumerable<JmInventoryTTran> GetAll();
        JmInventoryTTran Insert(JmInventoryTTran entity);
        JmInventoryTTran Update(JmInventoryTTran entity);
        JmInventoryTTran Delete(JmInventoryTTran entity);
        JmInventoryTTran One(string id);
        IEnumerable<JmInventoryTTran> GetListNotDeleted();

        IEnumerable<JmInventoryTTran> GetListByTransType(string TransType);

        IEnumerable<JmInventoryTTran> GetListByTransTypeAndStatus(string TransType, string TransStatus);
    }
}
