using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryTProductPriceTasks
    {
       IEnumerable<JmInventoryTProductPrice> GetAll();
        JmInventoryTProductPrice Insert(JmInventoryTProductPrice entity);
        JmInventoryTProductPrice Update(JmInventoryTProductPrice entity);
        JmInventoryTProductPrice Delete(JmInventoryTProductPrice entity);
        JmInventoryTProductPrice One(string id);
        IEnumerable<JmInventoryTProductPrice> GetListNotDeleted();

        IEnumerable<JmInventoryTProductPrice> GetListByProductId(string ParentProductId);
    }
}
