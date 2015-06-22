﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Moonlit.Mvc.Maintenance.Properties;

namespace Moonlit.Mvc.Maintenance.Domains
{
    public class User : IIdentity, IUser
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }

        public bool IsEnabled
        {
            get;
            set;

        }
        public string Password { get; set; }

        public int CultureId { get; set; }
        [Display(ResourceType = typeof(CultureTextResources), Name = "AdminUserGender")]
        public Gender? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string HashPassword(string rawString)
        {
            byte[] salted = Encoding.UTF8.GetBytes(string.Concat(rawString, this.LoginName));

            SHA256 hasher = new SHA256Managed();
            byte[] hashed = hasher.ComputeHash(salted);

            return Convert.ToBase64String(hashed);
        }
        public string UserName { get; set; }

        public bool IsSuper { get; set; }
        [DbContextExport(Ignore = true)]
        public virtual ICollection<Role> Roles { get; set; }

        string IIdentity.Name
        {
            get { return this.LoginName; }
        }

        string IIdentity.AuthenticationType
        {
            get { return "Maint"; }
        }
        bool IIdentity.IsAuthenticated
        {
            get { return true; }
        }

        public string Avatar { get; set; }

        public bool IsBuildIn { get; set; }
    }
}