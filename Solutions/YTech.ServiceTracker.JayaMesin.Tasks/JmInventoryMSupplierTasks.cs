using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;
using SharpArch.Domain;
using YTech.ServiceTracker.JayaMesin.Infrastructure.Repository;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Tasks
{
     public class JmInventoryMSupplierTasks : IJmInventoryMSupplierTasks
    {
      private readonly IJmInventoryMSupplierRepository _JmInventoryMSupplierRepository;

        public JmInventoryMSupplierTasks(IJmInventoryMSupplierRepository JmInventoryMSupplierRepository)
        {
            this._JmInventoryMSupplierRepository = JmInventoryMSupplierRepository;
        }

        public IEnumerable<Domain.JmInventoryMSupplier> GetAll()
        {
            var entitys = this._JmInventoryMSupplierRepository.GetAll(); ;
            return entitys;
        }
        
        public JmInventoryMSupplier Insert(Domain.JmInventoryMSupplier entity)
        {
            _JmInventoryMSupplierRepository.DbContext.BeginTransaction();
            _JmInventoryMSupplierRepository.Save(entity);
            _JmInventoryMSupplierRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMSupplier Update(Domain.JmInventoryMSupplier entity)
        {
            _JmInventoryMSupplierRepository.DbContext.BeginTransaction();
            _JmInventoryMSupplierRepository.Update(entity);
            _JmInventoryMSupplierRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMSupplier Delete(Domain.JmInventoryMSupplier entity)
        {
            _JmInventoryMSupplierRepository.DbContext.BeginTransaction();
            _JmInventoryMSupplierRepository.Delete(entity);
            _JmInventoryMSupplierRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMSupplier One(string id)
        {
            var entitys = this._JmInventoryMSupplierRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmInventoryMSupplier> GetListNotDeleted()
        {
            var entitys = this._JmInventoryMSupplierRepository.GetListNotDeleted(); ;
            return entitys;
        }

        public JmInventoryMSupplier GetLastCreatedSupplier()
        {
            JmInventoryMSupplier supp = this._JmInventoryMSupplierRepository.GetLastCreatedSupplier();
            return supp;
        }
    }
}
