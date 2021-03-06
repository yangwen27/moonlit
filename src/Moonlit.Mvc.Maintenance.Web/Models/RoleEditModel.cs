using System.Collections.Generic;
using System.Web.Mvc;
using Moonlit.Mvc.Controls;
using Moonlit.Mvc.Maintenance.Domains;
using Moonlit.Mvc.Maintenance.Properties;
using Moonlit.Mvc.Templates;

namespace Moonlit.Mvc.Maintenance.Models
{
    public partial class RoleEditModel : IEntityMapper<Role>
    {
        partial void OnTemplate(AdministrationSimpleEditTemplate template, ControllerContext controllerContext)
        {
            template.Buttons = new List<IClickable>
            {
                new Button(MaintCultureTextResources.Save ),
            };
        }
        private string[] MappingPrivilegeArrayFromEntity(Role entity, ControllerContext controllerContext)
        {
            return entity.PrivilegeArray;
        }

        private string[] MappingPrivilegeArrayToEntity(Role entity, ControllerContext controllerContext)
        {
            return this.PrivilegeArray;
        }
    }
}