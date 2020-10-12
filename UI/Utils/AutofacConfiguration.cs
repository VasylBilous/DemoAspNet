using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using BLL.Services.Abstraction;
using BLL.Services.Implementation;
using DAL.Entities;
using DAL.Repository.Abstraction;
using DAL.Repository.Implementation;
using System.Data.Entity;
using System.Web.Mvc;

namespace UI.Utils
{
    public class AutofacConfiguration
    {
        public static void Configurate()
        {
            // 1. ContainerBuilder
            var builder = new ContainerBuilder();
            // 2. Register controllers in app
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            // 3. Register types
            builder.RegisterType<ApplicationContext>().As<DbContext>().SingleInstance();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IGenericRepository<>));
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<GenreService>().As<IGenreService>();
            builder.RegisterType<DeveloperService>().As<IDeveloperService>();
            builder.RegisterType<CartService>().As<ICartService>();

            var configMapper = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperConfig()));
            builder.RegisterInstance(configMapper.CreateMapper());

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}
