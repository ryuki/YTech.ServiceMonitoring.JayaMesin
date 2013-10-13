using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface IMCustomerTasks
    {
        IEnumerable<MCustomer> GetAllCustomers();
        MCustomer Insert(MCustomer cust);
        MCustomer Update(MCustomer cust);
        MCustomer Delete(MCustomer cust);
        MCustomer One(string custId);
        MCustomer GetLastCreatedCustomer();
        IEnumerable<MCustomer> GetListNotDeleted();
    }
}
