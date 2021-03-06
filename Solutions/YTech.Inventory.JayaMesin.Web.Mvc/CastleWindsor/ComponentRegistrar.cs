﻿using SharpArch.Web.Mvc.Castle;

namespace YTech.Inventory.JayaMesin.Web.Mvc.CastleWindsor
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using SharpArch.Domain.PersistenceSupport;
    using SharpArch.NHibernate;
    using SharpArch.NHibernate.Contracts.Repositories;

    public class ComponentRegistrar
    {
        public static void AddComponentsTo(IWindsorContainer container)
        {
            AddGenericRepositoriesTo(container);
            AddCustomRepositoriesTo(container);
            AddQueryObjectsTo(container);
            AddTasksTo(container);
            AddCommandsTo(container);
        }

        private static void AddTasksTo(IWindsorContainer container)
        {
            container.Register(
                AllTypes
                    .FromAssemblyNamed("YTech.ServiceTracker.JayaMesin.Tasks")
                    .Pick()
                    .WithService.FirstNonGenericCoreInterface("YTech.ServiceTracker.JayaMesin.Domain"));
        }

        private static void AddCustomRepositoriesTo(IWindsorContainer container)
        {
            container.Register(
                AllTypes
                    .FromAssemblyNamed("YTech.ServiceTracker.JayaMesin.Infrastructure")
                    .Pick()
                    .WithService.FirstNonGenericCoreInterface("YTech.ServiceTracker.JayaMesin.Domain"));
        }

        private static void AddGenericRepositoriesTo(IWindsorContainer container)
        {
            container.Register(
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(NHibernateQuery<>))
                    .Named("NHibernateQuery"));

            container.Register(
                Component.For(typeof(IEntityDuplicateChecker))
                    .ImplementedBy(typeof(EntityDuplicateChecker))
                    .Named("entityDuplicateChecker"));

            container.Register(
                Component.For(typeof(INHibernateRepository<>))
                    .ImplementedBy(typeof(NHibernateRepository<>))
                    .Named("nhibernateRepositoryType")
                    .Forward(typeof(IRepository<>)));

            container.Register(
                Component.For(typeof(INHibernateRepositoryWithTypedId<,>))
                    .ImplementedBy(typeof(NHibernateRepositoryWithTypedId<,>))
                    .Named("nhibernateRepositoryWithTypedId")
                    .Forward(typeof(IRepositoryWithTypedId<,>)));

            container.Register(
                    Component.For(typeof(ISessionFactoryKeyProvider))
                        .ImplementedBy(typeof(DefaultSessionFactoryKeyProvider))
                        .Named("sessionFactoryKeyProvider"));

            container.Register(
                    Component.For(typeof(SharpArch.Domain.Commands.ICommandProcessor))
                        .ImplementedBy(typeof(SharpArch.Domain.Commands.CommandProcessor))
                        .Named("commandProcessor"));

        }

        private static void AddQueryObjectsTo(IWindsorContainer container)
        {
            container.Register(
                AllTypes.FromAssemblyNamed("YTech.Inventory.JayaMesin.Web.Mvc")
                    .Pick()
                    .WithService.FirstInterface());
        }

        private static void AddCommandsTo(IWindsorContainer container)
        {
            container.Register(
                AllTypes.FromAssemblyNamed("YTech.ServiceTracker.JayaMesin.Tasks")
                    .Pick()
                    .WithService.FirstInterface());
        }
    }
}