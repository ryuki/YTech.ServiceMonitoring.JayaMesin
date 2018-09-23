using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryMExpeditionTasks
    {
       IEnumerable<JmInventoryMExpedition> GetAll();
        JmInventoryMExpedition Insert(JmInventoryMExpedition entity);
        JmInventoryMExpedition Update(JmInventoryMExpedition entity);
        JmInventoryMExpedition Delete(JmInventoryMExpedition entity);
        JmInventoryMExpedition One(string id);
        IEnumerable<JmInventoryMExpedition> GetListNotDeleted();

        JmInventoryMExpedition GetLastCreatedExpedition();
    }
}
