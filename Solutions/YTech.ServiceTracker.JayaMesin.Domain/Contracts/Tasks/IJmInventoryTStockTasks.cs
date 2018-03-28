using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryTStockTasks
    {
       IEnumerable<JmInventoryTStock> GetAll();
        JmInventoryTStock Insert(JmInventoryTStock entity);
        JmInventoryTStock Update(JmInventoryTStock entity);
        JmInventoryTStock Delete(JmInventoryTStock entity);
        JmInventoryTStock One(string id);
        IEnumerable<JmInventoryTStock> GetListNotDeleted();

        IEnumerable<JmInventoryTStock> GetListByTransDetId(string TransDetId);
    }
}
