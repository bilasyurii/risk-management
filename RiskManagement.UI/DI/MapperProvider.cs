using System;
using AutoMapper;
using Ninject.Activation;
using RiskManagement.UI.Base;

namespace RiskManagement.UI.DI
{
    public class MapperProvider : Provider<IMapper>
    {
        protected override IMapper CreateInstance(IContext context)
        {
            return new Mapping().Create();
        }
    }
}
