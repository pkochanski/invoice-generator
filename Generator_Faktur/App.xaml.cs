
using Autofac;
using Autofac.Features.ResolveAnything;
using Generator_Faktur.Application.Services;
using Generator_Faktur.Configurations;
using Generator_Faktur.ViewModels;
using Generator_Faktur.ViewModels.Locators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Generator_Faktur
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IocKernel.Init(new IocConfiguration());
            base.OnStartup(e);
        }
    }
}
