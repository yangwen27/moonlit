using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Moonlit.Caching;
using Moonlit.Mvc.Controls;
using Moonlit.Mvc.Maintenance.Properties;
using Moonlit.Mvc.Templates;

namespace Moonlit.Mvc.Maintenance.Models
{
    public class CacheIndexModel : IPagedRequest
    {
        public CacheIndexModel()
        {
            PageIndex = 1;
            PageSize = 10;
            OrderBy = "Name";
        }

        [Field(FieldWidth.W4)]
        [TextBox]
        [Display(ResourceType = typeof(MaintCultureTextResources), Name = "CacheName")]
        public string Name { get; set; }

        public string OrderBy { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public Template CreateTemplate(ControllerContext controllerContext, ICacheManager cacheManager, CacheKeyManager cacheKeyManager)
        {
            var query = cacheKeyManager.AllKeys.Select(x => new
            {
                Name = x,
                IsNull = !cacheManager.Exist(x)
            }).ToList().AsQueryable();

            if (!string.IsNullOrWhiteSpace(Name))
            {
                var name = Name.Trim();

                query = query.Where(x => x.Name.Contains(name));
            }

            var template = new AdministrationSimpleListTemplate(query)
            {
                Title = MaintCultureTextResources.CacheIndex,
                Description = MaintCultureTextResources.CacheIndexDescription,
                QueryPanelTitle = MaintCultureTextResources.PanelQuery,
                DefaultSort = "Name",
                DefaultPageSize = 10,
                Criteria = new FieldsBuilder().ForEntity(this, controllerContext).Build(),
                GlobalButtons = new List<IClickable>
                {
                    new Button(MaintCultureTextResources.Search),
                    new Button(MaintCultureTextResources.Clear, "Clear"),
                },
                Table = new Table
                {
                    Columns = new List<TableColumn>
                    {
                        new TableColumn
                        {
                            Sort = "Name",
                            Header = MaintCultureTextResources.CacheName,
                            CellTemplate = x => new Literal
                            {
                                Text = ((dynamic) x.Target).Name
                            }
                        },
                        new TableColumn
                        {
                            Sort = "Exist",
                            Header = MaintCultureTextResources.Null,
                            CellTemplate = x => new Literal
                            {
                                Text = ((dynamic) x.Target).IsNull == true ? MaintCultureTextResources.Yes : MaintCultureTextResources.No
                            }
                        }

                    }
                }
            }; 
            return template;

        }

    }
}