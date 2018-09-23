using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryMProductSupplierTasks
    {
       IEnumerable<JmInventoryMProductSupplier> GetAll();
        JmInventoryMProductSupplier Insert(JmInventoryMProductSupplier entity);
        JmInventoryMProductSupplier Update(JmInventoryMProductSupplier entity);
        JmInventoryMProductSupplier Delete(JmInventoryMProductSupplier entity);
        JmInventoryMProductSupplier One(string id);
        IEnumerable<JmInventoryMProductSupplier> GetListNotDeleted();
    }
}
