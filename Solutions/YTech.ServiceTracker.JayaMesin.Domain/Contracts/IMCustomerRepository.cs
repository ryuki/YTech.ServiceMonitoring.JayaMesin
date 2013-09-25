using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts
{
    public interface IMCustomerRepository : INHibernateRepositoryWithTypedId<MCustomer, string>
    {
        //MCustomer Save(MCustomer cust);
    }
}
