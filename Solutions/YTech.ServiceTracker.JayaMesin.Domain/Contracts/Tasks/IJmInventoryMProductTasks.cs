using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryMProductTasks
    {
       IEnumerable<JmInventoryMProduct> GetAll();
        JmInventoryMProduct Insert(JmInventoryMProduct entity);
        JmInventoryMProduct Update(JmInventoryMProduct entity);
        JmInventoryMProduct Delete(JmInventoryMProduct entity);
        JmInventoryMProduct One(string id);
        IEnumerable<JmInventoryMProduct> GetListNotDeleted();

        IEnumerable<JmInventoryMProduct> GetListNotDeletedAndNotDisc();

        JmInventoryMProduct GetLastCreatedProduct();
    }
}
