﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Moonlit.Caching;
using Moonlit.Mvc.Controls;
using Moonlit.Mvc.Scripts;
using Moonlit.Mvc.Sitemap;
using Moonlit.Mvc.Styles;
using Moonlit.Mvc.Templates;
using Moonlit.Mvc.Themes;

namespace Moonlit.Mvc.Sample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var builder = new ContainerBuilder();
            builder.RegisterType<SessionCachingFlash>().As<IFlash>().SingleInstance();
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().SingleInstance();
            builder.RegisterType<TestAuthenticateProvider>().As<IAuthenticateProvider>();
            builder.RegisterType<Authenticate>().AsSelf();


            var controllerAssemblies = new[] { typeof(MvcApplication).Assembly };
            builder.RegisterControllers(controllerAssemblies).PropertiesAutowired();
            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(controllerAssemblies);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Set the dependency resolver to be Autofac.


            var clipOneTheme = new ClipOneTheme();
            Themes.Themes themes = new Themes.Themes();
            themes.Register(clipOneTheme);
            builder.Register(context => new DefaultThemeLoader("clip-one", themes)).As<IThemeLoader>().InstancePerRequest();

            builder.RegisterType<ScriptsLoader>().As<ScriptsLoader>().InstancePerRequest();
            builder.RegisterType<StylesLoader>().As<StylesLoader>().InstancePerRequest();
            builder.RegisterType<ReflectionSitemapsLoader>().As<SitemapsLoader>().InstancePerRequest();

            var container = builder.Build();
            RequestMappings.Current.Register(RouteTable.Routes);
            AuthenticationManager.Current.Register(new Authenticate(container.Resolve<ICacheManager>()), new TestAuthenticateProvider());
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }

    public class TestAuthenticateProvider : IAuthenticateProvider
    {
        public IUserPrincipal GetUserPrincipal(string name)
        {
            return new TestUserPrincipal(name);
        }
    }

    public class TestUserPrincipal : IUserPrincipal
    {
        private readonly string _name;

        public TestUserPrincipal(string name)
        {
            _name = name;
            this.Privileges = new string[] { "edit" };
        }

        public bool IsInRole(string role)
        {
            if (Privileges == null)
            {
                return false;
            }
            return Privileges.Any(x => string.Equals(x, role, StringComparison.OrdinalIgnoreCase));
        }

        public IIdentity Identity
        {
            get
            {
                return new GenericIdentity(this._name);
            }
        }

        public string[] Privileges { get; set; }
    }

    public class AutofacMoonlitDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        public AutofacMoonlitDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public object Resolve(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        public object Resolve(Type serviceType, string key)
        {
            return _container.ResolveNamed(key, serviceType);
        }

        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            //            return _container.Resolve<IEnumerable<serviceType>>()
            throw new NotSupportedException();
        }

        public void Release(object service)
        {

        }
    }

    public class ClipOneTheme : Theme
    {
        public ClipOneTheme()
        {
            this.RegisterControl(typeof(List), ThemeName + "/Controls/List");
            this.RegisterControl(typeof(TextBox), ThemeName + "/Controls/TextBox");
            this.RegisterControl(typeof(PasswordBox), ThemeName + "/Controls/PasswordBox");
            this.RegisterControl(typeof(Button), ThemeName + "/Controls/Button");
            this.RegisterControl(typeof(Field), ThemeName + "/Controls/Field");
            this.RegisterControl(typeof(Link), ThemeName + "/Controls/Link");
            this.RegisterControl(typeof(Pager), ThemeName + "/Controls/Pager");
            this.RegisterControl(typeof(ControlCollection), ThemeName + "/Controls/ControlCollection");
            this.RegisterControl(typeof(Form), ThemeName + "/Controls/Form");
            this.RegisterControl(typeof(Panel), ThemeName + "/Controls/Panel");
            this.RegisterControl(typeof(Table), ThemeName + "/Controls/Table");
            this.RegisterControl(typeof(Literal), ThemeName + "/Controls/Literal");
            this.RegisterControl(typeof(DropdownList), ThemeName + "/Controls/DropdownList");
            this.RegisterControl(typeof(CheckBox), ThemeName + "/Controls/CheckBox");
        }
        private const string ThemeName = "clip-one";
        protected override void PreRequest(RequestContext requestContext)
        {
            UrlHelper url = new UrlHelper(requestContext);
            var styles = DependencyResolver.Current.GetService<Styles.StylesLoader>().Styles;
            var scripts = DependencyResolver.Current.GetService<Scripts.ScriptsLoader>().Scripts;
            if (styles != null)
            {
                styles.RegisterStyleLink("plugins:bootstrap", new StyleLink() { Href = url.Content("~/assets/" + ThemeName + "/plugins/bootstrap/css/bootstrap.min.css") });
                styles.RegisterStyleLink("plugins:font-awesome", new StyleLink() { Href = url.Content("~/assets/" + ThemeName + "/plugins/font-awesome/css/font-awesome.min.css") });
                styles.RegisterStyleLink("fonts:style", new StyleLink() { Href = url.Content("~/assets/" + ThemeName + "/fonts/style.css") });
                styles.RegisterStyleLink("css:main", new StyleLink() { Href = url.Content("~/assets/" + ThemeName + "/css/main.css") });
                styles.RegisterStyleLink("css:main-responsive", new StyleLink() { Href = url.Content("~/assets/" + ThemeName + "/css/main-responsive.css") });
                styles.RegisterStyleLink("plugins:iCheck", new StyleLink() { Href = url.Content("~/assets/" + ThemeName + "/plugins/iCheck/skins/all.css") });
                styles.RegisterStyleLink("plugins:bootstrap-colorpalette", new StyleLink() { Href = url.Content("~/assets/" + ThemeName + "/plugins/bootstrap-colorpalette/css/bootstrap-colorpalette.css") });
                styles.RegisterStyleLink("plugins:perfect-scrollbar", new StyleLink() { Href = url.Content("~/assets/" + ThemeName + "/plugins/perfect-scrollbar/src/perfect-scrollbar.css") });
                styles.RegisterStyleLink("css:theme_light", new StyleLink() { Href = url.Content("~/assets/" + ThemeName + "/css/theme_light.css"), Id = "skin_color" });
                styles.RegisterStyleLink("css:print", new StyleLink() { Href = url.Content("~/assets/" + ThemeName + "/css/print.css"), Media = StyleLinkMedia.Print });

                styles.RegisterStyleLink("plugins:font-awesome-ie7", new StyleLink() { Href = url.Content("~/assets/" + ThemeName + "/plugins/font-awesome/css/font-awesome-ie7.min.css"), Criteria = new IeVersionCriteria(7, IeVersionCriteriaOperator.Eq) });
            }

            //
            if (scripts != null)
            {
                scripts.RegisterScript("plugins:respond", new Script() { Src = url.Content("~/assets/" + ThemeName + "/plugins/respond.min.js"), Criteria = new IeVersionCriteria(9, IeVersionCriteriaOperator.Lt) });
                scripts.RegisterScript("plugins:excanvas", new Script() { Src = url.Content("~/assets/" + ThemeName + "/plugins/excanvas.min.js"), Criteria = new IeVersionCriteria(9, IeVersionCriteriaOperator.Lt) });
                scripts.RegisterScript("plugins:jquery", new Script() { Src = url.Content("~/assets/" + ThemeName + "/plugins/jQuery-lib/1.10.2/jquery.min.js"), Criteria = new IeVersionCriteria(9, IeVersionCriteriaOperator.Lt) });
                scripts.RegisterScript("plugins:jquery", new Script() { Src = url.Content("~/assets/" + ThemeName + "/plugins/jQuery-lib/2.0.3/jquery.min.js"), Criteria = new IeVersionCriteria(9, IeVersionCriteriaOperator.Gte) });
                scripts.RegisterScript("plugins:jquery-ui", new Script() { Src = url.Content("~/assets/" + ThemeName + "/plugins/jquery-ui/jquery-ui-1.10.2.custom.min.js") });
                scripts.RegisterScript("plugins:bootstrap", new Script() { Src = url.Content("~/assets/" + ThemeName + "/plugins/bootstrap/js/bootstrap.min.js") });
                scripts.RegisterScript("plugins:bootstrap-hover-dropdown", new Script() { Src = url.Content("~/assets/" + ThemeName + "/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js") });
                scripts.RegisterScript("plugins:blockUI", new Script() { Src = url.Content("~/assets/" + ThemeName + "/plugins/blockUI/jquery.blockUI.js") });
                scripts.RegisterScript("plugins:perfect-scrollbar:mousewheel", new Script() { Src = url.Content("~/assets/" + ThemeName + "/plugins/perfect-scrollbar/src/jquery.mousewheel.js") });
                scripts.RegisterScript("plugins:perfect-scrollbar", new Script() { Src = url.Content("~/assets/" + ThemeName + "/plugins/perfect-scrollbar/src/perfect-scrollbar.js") });
                scripts.RegisterScript("plugins:less", new Script() { Src = url.Content("~/assets/" + ThemeName + "/plugins/less/less-1.5.0.min.js") });
                scripts.RegisterScript("plugins:jquery-cookie", new Script() { Src = url.Content("~/assets/" + ThemeName + "/plugins/jquery-cookie/jquery.cookie.js") });
                scripts.RegisterScript("plugins:bootstrap-colorpalette", new Script() { Src = url.Content("~/assets/" + ThemeName + "/plugins/bootstrap-colorpalette/js/bootstrap-colorpalette.js") });
                scripts.RegisterScript("js:main", new Script() { Src = url.Content("~/assets/" + ThemeName + "/js/main.js") });
            }
        }

        public override string Name
        {
            get { return "clip-one"; }
        }
    }
}
