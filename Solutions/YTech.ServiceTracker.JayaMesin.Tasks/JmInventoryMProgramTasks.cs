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
     public class JmInventoryMProgramTasks : IJmInventoryMProgramTasks
    {
      private readonly IJmInventoryMProgramRepository _JmInventoryMProgramRepository;

        public JmInventoryMProgramTasks(IJmInventoryMProgramRepository JmInventoryMProgramRepository)
        {
            this._JmInventoryMProgramRepository = JmInventoryMProgramRepository;
        }

        public IEnumerable<Domain.JmInventoryMProgram> GetAll()
        {
            var entitys = this._JmInventoryMProgramRepository.GetAll(); ;
            return entitys;
        }
        
        public JmInventoryMProgram Insert(Domain.JmInventoryMProgram entity)
        {
            _JmInventoryMProgramRepository.DbContext.BeginTransaction();
            _JmInventoryMProgramRepository.Save(entity);
            _JmInventoryMProgramRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMProgram Update(Domain.JmInventoryMProgram entity)
        {
            _JmInventoryMProgramRepository.DbContext.BeginTransaction();
            _JmInventoryMProgramRepository.Update(entity);
            _JmInventoryMProgramRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMProgram Delete(Domain.JmInventoryMProgram entity)
        {
            _JmInventoryMProgramRepository.DbContext.BeginTransaction();
            _JmInventoryMProgramRepository.Delete(entity);
            _JmInventoryMProgramRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMProgram One(string id)
        {
            var entitys = this._JmInventoryMProgramRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmInventoryMProgram> GetListNotDeleted()
        {
            var entitys = this._JmInventoryMProgramRepository.GetListNotDeleted(); ;
            return entitys;
        } 
    }
}
