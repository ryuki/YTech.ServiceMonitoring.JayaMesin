using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.Repository
{
   public class MCustomerRepository : NHibernateRepositoryWithTypedId<MCustomer, string>, IMCustomerRepository
    {
       //public MCustomer Save(MCustomer cust)
       //{
       //    this.DbContext.BeginTransaction();
       //    this.Save(cust);
       //    this.DbContext.CommitTransaction();

       //    return cust;
       //}
    }
}
