using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryTProductPriceLogTasks
    {
       IEnumerable<JmInventoryTProductPriceLog> GetAll();
        JmInventoryTProductPriceLog Insert(JmInventoryTProductPriceLog entity);
        JmInventoryTProductPriceLog Update(JmInventoryTProductPriceLog entity);
        JmInventoryTProductPriceLog Delete(JmInventoryTProductPriceLog entity);
        JmInventoryTProductPriceLog One(string id);
        IEnumerable<JmInventoryTProductPriceLog> GetListNotDeleted();
    }
}
