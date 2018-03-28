using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IMSmsTemplateTasks
    {
        IEnumerable<MSmsTemplate> GetAllSmsTemplates();
        MSmsTemplate Insert(MSmsTemplate cust);
        MSmsTemplate Update(MSmsTemplate cust);
        MSmsTemplate Delete(MSmsTemplate cust);
        MSmsTemplate One(string custId);
        IEnumerable<MSmsTemplate> GetListNotDeleted();
        MSmsTemplate GetByWOStatus(string woStatus);
    }
}
