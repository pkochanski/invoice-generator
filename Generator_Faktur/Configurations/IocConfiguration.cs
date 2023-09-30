using Generator_Faktur.Application.Services;
using Generator_Faktur.ViewModels;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Generator_Faktur.Configurations
{
    public class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IPdfService>().To<PdfService>().InSingletonScope();
            Bind<FormViewModel>().ToSelf().InTransientScope();
            Bind<IExchangeService>().To<ExchangeRateService>().InSingletonScope();
        }
    }
}
