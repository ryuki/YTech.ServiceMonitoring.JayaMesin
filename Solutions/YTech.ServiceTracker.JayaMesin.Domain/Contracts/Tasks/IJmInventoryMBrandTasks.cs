using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryMBrandTasks
    {
       IEnumerable<JmInventoryMBrand> GetAll();
        JmInventoryMBrand Insert(JmInventoryMBrand entity);
        JmInventoryMBrand Update(JmInventoryMBrand entity);
        JmInventoryMBrand Delete(JmInventoryMBrand entity);
        JmInventoryMBrand One(string id);
        IEnumerable<JmInventoryMBrand> GetListNotDeleted();

        JmInventoryMBrand GetLastCreatedBrand();
    }
}
