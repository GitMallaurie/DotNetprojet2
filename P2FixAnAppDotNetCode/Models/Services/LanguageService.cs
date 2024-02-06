using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// Provides services method to manage the application language
    /// </summary>
    public class LanguageService : ILanguageService
    {   
        /// <summary>
        /// Set the UI language
        /// </summary>
        public void ChangeUiLanguage(HttpContext context, string language)
        {
            string culture = SetCulture(language);
            UpdateCultureCookie(context, culture);           
        }

        /// <summary>
        /// Set the culture, EN as default
        /// </summary>
        public string SetCulture(string language)
        {
            string culture = "en";      
            
            if (string.Equals(language, "French"))
            {
                culture = "fr";
            }
            if (string.Equals(language,"Spanish"))
            {
                culture = "es";
            }
            else if (string.Equals(language, "English"))
            {
                culture = "en";
            }   
            
            return culture;          
        }

        /// <summary>
        /// Update the culture cookie
        /// </summary>
        public void UpdateCultureCookie(HttpContext context, string culture)
        {
            context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)));
        }
    }
}
