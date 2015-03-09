namespace QuestionBase.IOC
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Autofac;
    using QuestionBase.Services;
    using QuestionBase.Services.Services;
    using Repository;
    using Repository.Entities;
    using Repository.Repositories;
    using RepositoryCommon;

    public static class AutofacIoc
    {
        public static void Initialiser(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(DbContextFactory)).As<IDbContextFactory<DbContext>>().InstancePerRequest();
            
            builder.RegisterType<RepositoryBase<QuestionEntity>>().As<IRepositoryBase<QuestionEntity>>().WithParameter("dbContextFactory", new DbContextFactory()); ;
            builder.RegisterType<RepositoryBase<QuestionTypeEntity>>().As<IRepositoryBase<QuestionTypeEntity>>().WithParameter("dbContextFactory", new DbContextFactory()); ;

            builder.RegisterType(typeof(QuestionService)).As<IQuestionService>().InstancePerRequest();
            builder.RegisterType(typeof(QuestionTypeService)).As<IQuestionTypeService>().InstancePerRequest();

            builder.RegisterType(typeof(QuestionRepository)).As<IQuestionRepository>().InstancePerRequest();
            builder.RegisterType(typeof(QuestionTypeRepository)).As<IQuestionTypeRepository>().InstancePerRequest();
        }
    }
}
