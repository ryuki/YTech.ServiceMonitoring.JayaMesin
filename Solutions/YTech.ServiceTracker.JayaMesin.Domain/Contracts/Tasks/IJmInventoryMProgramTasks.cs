using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IJmInventoryMProgramTasks
    {
       IEnumerable<JmInventoryMProgram> GetAll();
        JmInventoryMProgram Insert(JmInventoryMProgram entity);
        JmInventoryMProgram Update(JmInventoryMProgram entity);
        JmInventoryMProgram Delete(JmInventoryMProgram entity);
        JmInventoryMProgram One(string id);
        IEnumerable<JmInventoryMProgram> GetListNotDeleted();
    }
}
