using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryMSupplierTasks
    {
       IEnumerable<JmInventoryMSupplier> GetAll();
        JmInventoryMSupplier Insert(JmInventoryMSupplier entity);
        JmInventoryMSupplier Update(JmInventoryMSupplier entity);
        JmInventoryMSupplier Delete(JmInventoryMSupplier entity);
        JmInventoryMSupplier One(string id);
        IEnumerable<JmInventoryMSupplier> GetListNotDeleted();
    }
}
