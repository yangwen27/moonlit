using System.Web.Mvc;
using Moonlit.Mvc.Maintenance.Models;
using Moonlit.Mvc.Maintenance.Properties;

namespace Moonlit.Mvc.Maintenance.Controllers
{
    [MoonlitAuthorize(Roles = MaintModule.PrivilegeAdminUser )]
    public class ExceptionLogController : MaintControllerBase
    {

        [SitemapNode(Parent = "Site", ResourceType = typeof(MaintCultureTextResources), Text = "ExceptionLogList")]
        public ActionResult Index(ExceptionLogListModel model)
        {
            return Template(model.CreateTemplate(ControllerContext));
        }
    }
}