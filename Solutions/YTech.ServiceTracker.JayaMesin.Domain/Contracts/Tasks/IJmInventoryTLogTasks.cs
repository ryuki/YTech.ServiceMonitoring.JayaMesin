using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Enums;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryTLogTasks
    {
       IEnumerable<JmInventoryTLog> GetAll();
        JmInventoryTLog Insert(JmInventoryTLog entity);
        JmInventoryTLog Update(JmInventoryTLog entity);
        JmInventoryTLog Delete(JmInventoryTLog entity);
        JmInventoryTLog One(string id);
        IEnumerable<JmInventoryTLog> GetListNotDeleted();

        bool GetHaveBeenLogType(string refId, string userName, EnumLogType logType, EnumTransLog logStatus);
    }
}
