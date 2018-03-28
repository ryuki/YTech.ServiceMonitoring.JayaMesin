using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryTRedeemDetTasks
    {
       IEnumerable<JmInventoryTRedeemDet> GetAll();
        JmInventoryTRedeemDet Insert(JmInventoryTRedeemDet entity);
        JmInventoryTRedeemDet Update(JmInventoryTRedeemDet entity);
        JmInventoryTRedeemDet Delete(JmInventoryTRedeemDet entity);
        JmInventoryTRedeemDet One(string id);
        IEnumerable<JmInventoryTRedeemDet> GetListNotDeleted();
    }
}
