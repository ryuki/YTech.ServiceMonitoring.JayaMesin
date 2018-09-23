using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain;
using YTech.ServiceTracker.JayaMesin.Infrastructure.Repository;
using YTech.ServiceTracker.JayaMesin.Domain;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts;

namespace YTech.ServiceTracker.JayaMesin.Tasks
{
     public class JmInventoryMExpeditionTasks : IJmInventoryMExpeditionTasks
    {
      private readonly IJmInventoryMExpeditionRepository _JmInventoryMExpeditionRepository;

        public JmInventoryMExpeditionTasks(IJmInventoryMExpeditionRepository JmInventoryMExpeditionRepository)
        {
            this._JmInventoryMExpeditionRepository = JmInventoryMExpeditionRepository;
        }

        public IEnumerable<Domain.JmInventoryMExpedition> GetAll()
        {
            var entitys = this._JmInventoryMExpeditionRepository.GetAll(); ;
            return entitys;
        }
        
        public JmInventoryMExpedition Insert(Domain.JmInventoryMExpedition entity)
        {
            _JmInventoryMExpeditionRepository.DbContext.BeginTransaction();
            _JmInventoryMExpeditionRepository.Save(entity);
            _JmInventoryMExpeditionRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMExpedition Update(Domain.JmInventoryMExpedition entity)
        {
            _JmInventoryMExpeditionRepository.DbContext.BeginTransaction();
            _JmInventoryMExpeditionRepository.Update(entity);
            _JmInventoryMExpeditionRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMExpedition Delete(Domain.JmInventoryMExpedition entity)
        {
            _JmInventoryMExpeditionRepository.DbContext.BeginTransaction();
            _JmInventoryMExpeditionRepository.Delete(entity);
            _JmInventoryMExpeditionRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMExpedition One(string id)
        {
            var entitys = this._JmInventoryMExpeditionRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmInventoryMExpedition> GetListNotDeleted()
        {
            var entitys = this._JmInventoryMExpeditionRepository.GetListNotDeleted(); ;
            return entitys;
        }


        public JmInventoryMExpedition GetLastCreatedExpedition()
        {
            JmInventoryMExpedition ent = this._JmInventoryMExpeditionRepository.GetLastCreatedExpedition();
            return ent;
        }
    }
}
