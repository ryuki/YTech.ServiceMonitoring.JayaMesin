using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryTReferenceTasks
    {
       IEnumerable<JmInventoryTReference> GetAll();
        JmInventoryTReference Insert(JmInventoryTReference entity);
        JmInventoryTReference Update(JmInventoryTReference entity);
        JmInventoryTReference Delete(JmInventoryTReference entity);
        JmInventoryTReference One(string id);
        IEnumerable<JmInventoryTReference> GetListNotDeleted();

        JmInventoryTReference GetByReferenceType(Enums.EnumInventoryReferenceType enumInventoryReferenceType);
    }
}
