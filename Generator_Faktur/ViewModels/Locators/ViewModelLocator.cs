using System;
using System.Collections.Generic;
using System.Text;
using Ninject;
using Ninject.Modules;

namespace Generator_Faktur.ViewModels.Locators
{
    public class ViewModelLocator
    {
        public FormViewModel FormViewModel { 
            get { return IocKernel.Get<FormViewModel>(); } 
        }
    }

    public static class IocKernel
    {
        private static IKernel kernel;
        public static void Init(params NinjectModule[] modules)
        {
            if(kernel is null)
            {
                kernel = new StandardKernel(modules);
            }
        }

        public static T Get<T>()
        {
            return kernel.Get<T>();
        }
    }
}
