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
     public class JmInventoryMCatTasks : IJmInventoryMCatTasks
    {
      private readonly IJmInventoryMCatRepository _JmInventoryMCatRepository;

        public JmInventoryMCatTasks(IJmInventoryMCatRepository JmInventoryMCatRepository)
        {
            this._JmInventoryMCatRepository = JmInventoryMCatRepository;
        }

        public IEnumerable<Domain.JmInventoryMCat> GetAll()
        {
            var entitys = this._JmInventoryMCatRepository.GetAll(); ;
            return entitys;
        }
        
        public JmInventoryMCat Insert(Domain.JmInventoryMCat entity)
        {
            _JmInventoryMCatRepository.DbContext.BeginTransaction();
            _JmInventoryMCatRepository.Save(entity);
            _JmInventoryMCatRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMCat Update(Domain.JmInventoryMCat entity)
        {
            _JmInventoryMCatRepository.DbContext.BeginTransaction();
            _JmInventoryMCatRepository.Update(entity);
            _JmInventoryMCatRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMCat Delete(Domain.JmInventoryMCat entity)
        {
            _JmInventoryMCatRepository.DbContext.BeginTransaction();
            _JmInventoryMCatRepository.Delete(entity);
            _JmInventoryMCatRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMCat One(string id)
        {
            var entitys = this._JmInventoryMCatRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmInventoryMCat> GetListNotDeleted()
        {
            var entitys = this._JmInventoryMCatRepository.GetListNotDeleted(); ;
            return entitys;
        }


        public JmInventoryMCat GetLastCreatedCat()
        {
            JmInventoryMCat ent = this._JmInventoryMCatRepository.GetLastCreatedCat();
            return ent;
        }
    }
}
