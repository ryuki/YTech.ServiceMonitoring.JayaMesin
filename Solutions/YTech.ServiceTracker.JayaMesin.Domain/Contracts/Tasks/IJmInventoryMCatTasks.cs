using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryMCatTasks
    {
       IEnumerable<JmInventoryMCat> GetAll();
        JmInventoryMCat Insert(JmInventoryMCat entity);
        JmInventoryMCat Update(JmInventoryMCat entity);
        JmInventoryMCat Delete(JmInventoryMCat entity);
        JmInventoryMCat One(string id);
        IEnumerable<JmInventoryMCat> GetListNotDeleted();

        JmInventoryMCat GetLastCreatedCat();
    }
}
