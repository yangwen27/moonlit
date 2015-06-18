﻿using Moonlit.Mvc.Maintenance.Properties;

namespace Moonlit.Mvc.Maintenance.Domains
{
    public static class DomainExtensions
    {
        public static string ToDisplayString(this Gender gender)
        {
            return gender == Gender.Male ? CultureTextResources.GenderMale : CultureTextResources.GenderFemale;

        }
        public static string ToDisplayString(this Gender? gender)
        {
            if (gender == null)
            {
                return string.Empty;
            }
            return ToDisplayString(gender.Value);
        }
    }
}