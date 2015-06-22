using System.Web.Mvc;
using Autofac;

namespace Moonlit.Mvc.Maintenance
{
    internal class MaintModuleConfiguration : IModuleConfiguration
    {
        public void Configure(IContainer container)
        {
            AuthorizeManager.Setup();
             
            Moonlit.Properties.CultureTextResources.LanguageLoader = DependencyResolver.Current.GetService<ILanguageLoader>(false) ??
                                                  new NullLanguageLoader();
        }
    }
}