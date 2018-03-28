using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryTTransDetTasks
    {
       IEnumerable<JmInventoryTTransDet> GetAll();
        JmInventoryTTransDet Insert(JmInventoryTTransDet entity);
        JmInventoryTTransDet Update(JmInventoryTTransDet entity);
        JmInventoryTTransDet Delete(JmInventoryTTransDet entity);
        JmInventoryTTransDet One(string id);
        IEnumerable<JmInventoryTTransDet> GetListNotDeleted();

        IEnumerable<JmInventoryTTransDet> GetListByTransId(string TransId);

        IEnumerable<JmInventoryTTransDet> GetListByTransDetId(string TransDetId);
    }
}
