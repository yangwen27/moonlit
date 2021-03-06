﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Moonlit.Mvc.Templates;

namespace Moonlit.Mvc.Html
{
    public static class RenderExtensions
    {
        public static IHtmlString Render(this HtmlHelper html, Control control)
        {
            if (control == null)
            {
                return MvcHtmlString.Empty;
            }
            var theme = Theme.Current;
            var template = theme.ResolveControl(control.GetType());
            if (string.IsNullOrEmpty(template))
            {
                return MvcHtmlString.Create("there is no template for control " + control.GetType().FullName);
            }
            return html.Partial(template, control);

        }
    }
}
