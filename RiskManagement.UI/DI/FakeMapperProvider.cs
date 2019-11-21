using System;
using AutoMapper;
using Ninject.Activation;

namespace RiskManagement.UI.DI
{
    public class FakeMapperProvider : Provider<IMapper>
    {
        protected override IMapper CreateInstance(IContext context)
        {
            return null;
        }
    }
}
