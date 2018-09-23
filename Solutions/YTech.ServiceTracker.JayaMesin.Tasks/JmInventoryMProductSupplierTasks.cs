using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain;
using YTech.ServiceTracker.JayaMesin.Infrastructure.Repository;
using YTech.ServiceTracker.JayaMesin.Domain;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;

namespace YTech.ServiceTracker.JayaMesin.Tasks
{
     public class JmInventoryMProductSupplierTasks : IJmInventoryMProductSupplierTasks
    {
      private readonly IJmInventoryMProductSupplierRepository _JmInventoryMProductSupplierRepository;

        public JmInventoryMProductSupplierTasks(IJmInventoryMProductSupplierRepository JmInventoryMProductSupplierRepository)
        {
            this._JmInventoryMProductSupplierRepository = JmInventoryMProductSupplierRepository;
        }

        public IEnumerable<Domain.JmInventoryMProductSupplier> GetAll()
        {
            var entitys = this._JmInventoryMProductSupplierRepository.GetAll(); ;
            return entitys;
        }
        
        public JmInventoryMProductSupplier Insert(Domain.JmInventoryMProductSupplier entity)
        {
            _JmInventoryMProductSupplierRepository.DbContext.BeginTransaction();
            _JmInventoryMProductSupplierRepository.Save(entity);
            _JmInventoryMProductSupplierRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMProductSupplier Update(Domain.JmInventoryMProductSupplier entity)
        {
            _JmInventoryMProductSupplierRepository.DbContext.BeginTransaction();
            _JmInventoryMProductSupplierRepository.Update(entity);
            _JmInventoryMProductSupplierRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMProductSupplier Delete(Domain.JmInventoryMProductSupplier entity)
        {
            _JmInventoryMProductSupplierRepository.DbContext.BeginTransaction();
            _JmInventoryMProductSupplierRepository.Delete(entity);
            _JmInventoryMProductSupplierRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMProductSupplier One(string id)
        {
            var entitys = this._JmInventoryMProductSupplierRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmInventoryMProductSupplier> GetListNotDeleted()
        {
            var entitys = this._JmInventoryMProductSupplierRepository.GetListNotDeleted(); ;
            return entitys;
        } 
    }
}
