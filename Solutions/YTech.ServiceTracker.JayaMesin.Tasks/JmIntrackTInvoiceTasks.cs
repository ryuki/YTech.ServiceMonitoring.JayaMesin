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
    public class JmIntrackTInvoiceTasks : IJmIntrackTInvoiceTasks
    {
        private readonly IJmIntrackTInvoiceRepository _JmIntrackTInvoiceRepository;

        public JmIntrackTInvoiceTasks(IJmIntrackTInvoiceRepository JmIntrackTInvoiceRepository)
        {
            this._JmIntrackTInvoiceRepository = JmIntrackTInvoiceRepository;
        }

        public IEnumerable<Domain.JmIntrackTInvoice> GetAll()
        {
            var entitys = this._JmIntrackTInvoiceRepository.GetAll(); ;
            return entitys;
        }

        public JmIntrackTInvoice Insert(Domain.JmIntrackTInvoice entity)
        {
            _JmIntrackTInvoiceRepository.DbContext.BeginTransaction();
            _JmIntrackTInvoiceRepository.Save(entity);
            _JmIntrackTInvoiceRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmIntrackTInvoice Update(Domain.JmIntrackTInvoice entity)
        {
            _JmIntrackTInvoiceRepository.DbContext.BeginTransaction();
            _JmIntrackTInvoiceRepository.Update(entity);
            _JmIntrackTInvoiceRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmIntrackTInvoice Delete(Domain.JmIntrackTInvoice entity)
        {
            _JmIntrackTInvoiceRepository.DbContext.BeginTransaction();
            _JmIntrackTInvoiceRepository.Delete(entity);
            _JmIntrackTInvoiceRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmIntrackTInvoice One(string id)
        {
            var entitys = this._JmIntrackTInvoiceRepository.Get(id);
            return entitys;
        }

        public IEnumerable<JmIntrackTInvoice> GetListNotDeleted()
        {
            var entitys = this._JmIntrackTInvoiceRepository.GetListNotDeleted(); ;
            return entitys;
        }


        public void AutoUpdateInvoiceStatus()
        {
            this._JmIntrackTInvoiceRepository.AutoUpdateInvoiceStatus();
        }


        public IEnumerable<JmIntrackTInvoiceHaveRead> GetListHaveBeenRead(string UserName, string TextSearch)
        {
            var entitys = this._JmIntrackTInvoiceRepository.GetListHaveBeenRead(UserName, TextSearch); ;
            return entitys;
        }
    }
}
