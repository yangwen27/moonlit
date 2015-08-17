﻿ 

using System.Web.Mvc; 
using System; 
using System.ComponentModel.DataAnnotations;
using Moonlit.Mvc;
using Moonlit.Mvc.Templates;
using Moonlit.Mvc.Maintenance.Domains;
using Moonlit.Mvc.Controls;


using Moonlit.Mvc.Maintenance.Properties;
using Moonlit.Mvc.Maintenance.SelectListItemsProviders;

namespace Moonlit.Mvc.Maintenance.Models
{

	public partial class AdminUserListModel  {
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "Keyword"
			)]
		[Field(FieldWidth.W6)]
 
		[TextBox] 
		public string Keyword { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserUserName"
			)]
		[Field(FieldWidth.W6)]
 
		[TextBox] 
		public string UserName { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserIsEnabled"
			)]
		[Field(FieldWidth.W6)]
 
		[CheckBox] 
		public bool? IsEnabled { get; set; }
		partial void OnTemplate(AdministrationSimpleListTemplate template, ControllerContext controllerContext);

		public Template CreateTemplate(ControllerContext controllerContext)
        {
            var query = GetDataSource(controllerContext);
            var template = new AdministrationSimpleListTemplate(query)
            { 
                Title = Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources.AdminUserList,
                Description = Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources.AdminUserListDescription,
                QueryPanelTitle = Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources.PanelQuery,
                DefaultSort = OrderBy,
                DefaultPageSize = PageSize,
                Criteria = new FieldsBuilder().ForEntity(this, controllerContext).Build(), 
            }; 
			
			OnTemplate (template, controllerContext);
            return template;
        }
 
	} 
	public partial class AdminUserCreateModel  {
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserUserName"
			)]
		[Field(FieldWidth.W6)]
		public string UserName { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserLoginName"
			)]
		[Field(FieldWidth.W6)]
		public string LoginName { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserPassword"
			)]
		[Field(FieldWidth.W6)]
 
		[PasswordBox] 
		public string Password { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserGender"
			)]
		[Field(FieldWidth.W6)]
		public Gender? Gender { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserDateOfBirth"
			)]
		[Field(FieldWidth.W6)]
		public DateTime? DateOfBirth { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserCulture"
			)]
		[Field(FieldWidth.W6)]
 
		[SelectList(typeof(CultureSelectListProvider))] 
		public int CultureId { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserIsSuper"
			)]
		[Field(FieldWidth.W6)]
		public bool IsSuper { get; private set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserRoles"
			)]
		[Field(FieldWidth.W6)]
 
		[MultiSelectList(typeof(RoleSelectListProvider))] 
		public int[] Roles { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserIsEnabled"
			)]
		[Field(FieldWidth.W6)]
		public bool IsEnabled { get; set; }
		partial void OnTemplate(AdministrationSimpleEditTemplate template, ControllerContext controllerContext); 
		public Template CreateTemplate(ControllerContext controllerContext)
		{ 
			var template = new AdministrationSimpleEditTemplate
			{
                Title = Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources.AdminUserEdit,
                Description = Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources.AdminUserEditDescription,
				FormTitle = MaintCultureTextResources.AdminUserInfo,
				Fields = new FieldsBuilder().ForEntity(this, controllerContext).Build(),
			};
			OnTemplate(template, controllerContext);
			return template;
		}
		partial void OnFromEntity(User entity, bool isPostback);
        public void FromEntity(User entity, bool isPostback)
        {
			if(!isPostback){
				UserName = entity.UserName;
				LoginName = entity.LoginName;
  
				Password = MappingPasswordFromEntity(entity);
				Gender = entity.Gender;
				DateOfBirth = entity.DateOfBirth;
				CultureId = entity.CultureId;
  
				Roles = MappingRolesFromEntity(entity);
				IsEnabled = entity.IsEnabled;
			}
			IsSuper = entity.IsSuper;
			OnFromEntity(entity, isPostback);
		}
		partial void OnToEntity(User entity);
        public void ToEntity(User entity )
        {
			entity.UserName = UserName;
			entity.LoginName = LoginName;
  
			entity.Password = MappingPasswordToEntity(entity);
			entity.Gender = Gender;
			entity.DateOfBirth = DateOfBirth;
			entity.CultureId = CultureId;
  
			entity.Roles = MappingRolesToEntity(entity);
			entity.IsEnabled = IsEnabled;
			OnToEntity(entity);
		}
 
	} 
	public partial class AdminUserEditModel  {
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserUserName"
			)]
		[Field(FieldWidth.W6)]
		public string UserName { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserLoginName"
			)]
		[Field(FieldWidth.W6)]
		public string LoginName { get; private set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserPassword"
			)]
		[Field(FieldWidth.W6)]
 
		[PasswordBox] 
		public string Password { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserGender"
			)]
		[Field(FieldWidth.W6)]
		public Gender? Gender { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserDateOfBirth"
			)]
		[Field(FieldWidth.W6)]
		public DateTime? DateOfBirth { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserCulture"
			)]
		[Field(FieldWidth.W6)]
 
		[SelectList(typeof(CultureSelectListProvider))] 
		public int CultureId { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserIsSuper"
			)]
		[Field(FieldWidth.W6)]
		public bool IsSuper { get; private set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserRoles"
			)]
		[Field(FieldWidth.W6)]
 
		[MultiSelectList(typeof(RoleSelectListProvider))] 
		public int[] Roles { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "AdminUserIsEnabled"
			)]
		[Field(FieldWidth.W6)]
		public bool IsEnabled { get; set; }
		partial void OnTemplate(AdministrationSimpleEditTemplate template, ControllerContext controllerContext); 
		public Template CreateTemplate(ControllerContext controllerContext)
		{ 
			var template = new AdministrationSimpleEditTemplate
			{
                Title = Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources.AdminUserEdit,
                Description = Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources.AdminUserEditDescription,
				FormTitle = MaintCultureTextResources.AdminUserInfo,
				Fields = new FieldsBuilder().ForEntity(this, controllerContext).Build(),
			};
			OnTemplate(template, controllerContext);
			return template;
		}
		partial void OnFromEntity(User entity, bool isPostback);
        public void FromEntity(User entity, bool isPostback)
        {
			if(!isPostback){
				UserName = entity.UserName;
  
				Password = MappingPasswordFromEntity(entity);
				Gender = entity.Gender;
				DateOfBirth = entity.DateOfBirth;
				CultureId = entity.CultureId;
  
				Roles = MappingRolesFromEntity(entity);
				IsEnabled = entity.IsEnabled;
			}
			LoginName = entity.LoginName;
			IsSuper = entity.IsSuper;
			OnFromEntity(entity, isPostback);
		}
		partial void OnToEntity(User entity);
        public void ToEntity(User entity )
        {
			entity.UserName = UserName;
  
			entity.Password = MappingPasswordToEntity(entity);
			entity.Gender = Gender;
			entity.DateOfBirth = DateOfBirth;
			entity.CultureId = CultureId;
  
			entity.Roles = MappingRolesToEntity(entity);
			entity.IsEnabled = IsEnabled;
			OnToEntity(entity);
		}
 
	} 
	public partial class RoleListModel  {
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "Keyword"
			)]
		[Field(FieldWidth.W6)]
 
		[TextBox] 
		public string Keyword { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "RoleIsEnabled"
			)]
		[Field(FieldWidth.W6)]
 
		[CheckBox] 
		public bool? IsEnabled { get; set; }
		partial void OnTemplate(AdministrationSimpleListTemplate template, ControllerContext controllerContext);

		public Template CreateTemplate(ControllerContext controllerContext)
        {
            var query = GetDataSource(controllerContext);
            var template = new AdministrationSimpleListTemplate(query)
            { 
                Title = Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources.RoleList,
                Description = Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources.RoleListDescription,
                QueryPanelTitle = Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources.PanelQuery,
                DefaultSort = OrderBy,
                DefaultPageSize = PageSize,
                Criteria = new FieldsBuilder().ForEntity(this, controllerContext).Build(), 
            }; 
			
			OnTemplate (template, controllerContext);
            return template;
        }
 
	} 
	public partial class RoleCreateModel  {
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "RoleName"
			)]
		[Field(FieldWidth.W6)]
		public string Name { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "RolePrivileges"
			)]
		[Field(FieldWidth.W6)]
 
		[MultiSelectList(typeof(PrivilegeSelectListProvider))] 
		public string[] PrivilegeArray { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "RoleIsEnabled"
			)]
		[Field(FieldWidth.W6)]
		public bool IsEnabled { get; set; }
		partial void OnTemplate(AdministrationSimpleEditTemplate template, ControllerContext controllerContext); 
		public Template CreateTemplate(ControllerContext controllerContext)
		{ 
			var template = new AdministrationSimpleEditTemplate
			{
                Title = Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources.RoleEdit,
                Description = Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources.RoleEditDescription,
				FormTitle = MaintCultureTextResources.RoleInfo,
				Fields = new FieldsBuilder().ForEntity(this, controllerContext).Build(),
			};
			OnTemplate(template, controllerContext);
			return template;
		}
		partial void OnFromEntity(Role entity, bool isPostback);
        public void FromEntity(Role entity, bool isPostback)
        {
			if(!isPostback){
				Name = entity.Name;
  
				PrivilegeArray = MappingPrivilegeArrayFromEntity(entity);
				IsEnabled = entity.IsEnabled;
			}
			OnFromEntity(entity, isPostback);
		}
		partial void OnToEntity(Role entity);
        public void ToEntity(Role entity )
        {
			entity.Name = Name;
  
			entity.PrivilegeArray = MappingPrivilegeArrayToEntity(entity);
			entity.IsEnabled = IsEnabled;
			OnToEntity(entity);
		}
 
	} 
	public partial class RoleEditModel  {
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "RoleName"
			)]
		[Field(FieldWidth.W6)]
		public string Name { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "RolePrivileges"
			)]
		[Field(FieldWidth.W6)]
 
		[MultiSelectList(typeof(PrivilegeSelectListProvider))] 
		public string[] PrivilegeArray { get; set; }
 
		[Display(
			ResourceType = typeof(Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources),
			Name = "RoleIsEnabled"
			)]
		[Field(FieldWidth.W6)]
		public bool IsEnabled { get; set; }
		partial void OnTemplate(AdministrationSimpleEditTemplate template, ControllerContext controllerContext); 
		public Template CreateTemplate(ControllerContext controllerContext)
		{ 
			var template = new AdministrationSimpleEditTemplate
			{
                Title = Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources.RoleEdit,
                Description = Moonlit.Mvc.Maintenance.Properties.MaintCultureTextResources.RoleEditDescription,
				FormTitle = MaintCultureTextResources.RoleInfo,
				Fields = new FieldsBuilder().ForEntity(this, controllerContext).Build(),
			};
			OnTemplate(template, controllerContext);
			return template;
		}
		partial void OnFromEntity(Role entity, bool isPostback);
        public void FromEntity(Role entity, bool isPostback)
        {
			if(!isPostback){
				Name = entity.Name;
  
				PrivilegeArray = MappingPrivilegeArrayFromEntity(entity);
				IsEnabled = entity.IsEnabled;
			}
			OnFromEntity(entity, isPostback);
		}
		partial void OnToEntity(Role entity);
        public void ToEntity(Role entity )
        {
			entity.Name = Name;
  
			entity.PrivilegeArray = MappingPrivilegeArrayToEntity(entity);
			entity.IsEnabled = IsEnabled;
			OnToEntity(entity);
		}
 
	} 
}

