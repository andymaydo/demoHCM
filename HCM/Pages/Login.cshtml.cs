using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using HCMDataAccess;
using Microsoft.Extensions.Configuration;
using HCM.Options;

namespace HCM.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private IHttpContextAccessor _httpContext;
        private ILogger<LoginModel> _logger;
        private ILoginData _loginData;
        private readonly IConfiguration _configuration;
        public CommonLocalizationService _commonLocalizer;

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public List<SelectListItem> AvailableCultures { get; set; }

        public class InputModel
        {
            
            [Display(Name = "Login_LabelUserName"), Required]
            public string UserName { get; set; }

            [Display(Name = "Login_LabelPassword"), Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Login_LabelLanguage")]
            public string Culture { get; set; }
        }



  
        private async Task<bool> SignInAsync(string userName, string password, string culture)
        {
            bool res;
            try
            {
                var LoginResponse = await _loginData.Login(userName, password);
                if (LoginResponse.Success)
                {
                    var claims = new[]
                    {
                        new Claim("UserID", LoginResponse.UserID.ToString()),
                        new Claim("ContactID", LoginResponse.ContactID.ToString()),
                        new Claim("FullName", string.IsNullOrEmpty(LoginResponse.FullName) ? "FullName":LoginResponse.FullName ),                        
                        new Claim(ClaimTypes.NameIdentifier, userName)
                    };

                    
                    //var userRoles = await _portalUser.UserRolesGetAsync(pUser.AdmUserID);
                    //foreach (var r in userRoles)
                    //{
                    //    claims.Add(new Claim(ClaimTypes.Role, r));
                    //}

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(claimsIdentity);


                    Response.Cookies.Append(
                        CookieRequestCultureProvider.DefaultCookieName,
                        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                    );


                    await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    res = true;
                }
                else
                {
                    res = false;
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogDebug(ex.StackTrace);

                res = false;
            }
            return res;
        }       

        public LoginModel(ILogger<LoginModel> logger, IHttpContextAccessor httpContext, CommonLocalizationService localizer, IConfiguration Configuration, ILoginData loginData)
        {
            _logger = logger;
            _httpContext = httpContext;
            _loginData = loginData;
            _commonLocalizer = localizer;
            _configuration = Configuration;
                       
            AvailableCultures = _configuration.GetSection("Cultures")
                .GetChildren()
                .Select(c => new SelectListItem { Value = c.Key, Text = c.Value })
                .ToList();

            Input = new InputModel();
            Input.Culture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
    }




        public IActionResult OnGet(string returnUrl = null)
        {
         
            //if (CultureInfo.CurrentCulture.Name != null)
            //{
            //    Input.Culture = CultureInfo.CurrentCulture.Name;
            //}
            //else
            //{
            //    Input.Culture = _locOptions.Value.DefaultRequestCulture.Culture.Name;
            //}

            ReturnUrl = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                try
                {
                    var signed = await SignInAsync(Input.UserName, Input.Password, Input.Culture);
                    if (signed)
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, @_commonLocalizer.Str["Login_ErrorLoginNotPossible"]);
                        return Page();
                    }
                }
               catch(Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}