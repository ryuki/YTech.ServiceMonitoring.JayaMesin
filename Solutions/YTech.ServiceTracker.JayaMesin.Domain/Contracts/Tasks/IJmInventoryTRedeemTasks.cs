using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryTRedeemTasks
    {
       IEnumerable<JmInventoryTRedeem> GetAll();
        JmInventoryTRedeem Insert(JmInventoryTRedeem entity);
        JmInventoryTRedeem Update(JmInventoryTRedeem entity);
        JmInventoryTRedeem Delete(JmInventoryTRedeem entity);
        JmInventoryTRedeem One(string id);
        IEnumerable<JmInventoryTRedeem> GetListNotDeleted();
    }
}
