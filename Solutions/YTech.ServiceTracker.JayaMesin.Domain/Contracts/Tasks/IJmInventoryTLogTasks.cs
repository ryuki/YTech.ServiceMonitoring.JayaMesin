using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryTLogTasks
    {
       IEnumerable<JmInventoryTLog> GetAll();
        JmInventoryTLog Insert(JmInventoryTLog entity);
        JmInventoryTLog Update(JmInventoryTLog entity);
        JmInventoryTLog Delete(JmInventoryTLog entity);
        JmInventoryTLog One(string id);
        IEnumerable<JmInventoryTLog> GetListNotDeleted();
    }
}
