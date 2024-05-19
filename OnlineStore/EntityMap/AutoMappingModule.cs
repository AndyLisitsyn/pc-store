using System;
using Autofac;
using AutoMapper;

namespace EntityMap
{
    public class AutoMappingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var mapperConfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<EntityToDtoMappingProfile>();
                    cfg.AddProfile<DtoToEntityMappingProfile>();
                    //etc...
                });
                mapperConfig.AssertConfigurationIsValid();
                return mapperConfig;
            })
            .AsSelf()
            .SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
        }
    }
}
