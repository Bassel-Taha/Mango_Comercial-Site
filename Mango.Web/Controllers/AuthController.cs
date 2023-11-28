using Mango.Web.Model;
using Mango.Web.Models;
using Mango.Web.services.Iservices;
using Mango.Web.utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _AuthService;
        private readonly ITockenProvider _TockenProvider;
        public AuthController(IAuthService AuthService, ITockenProvider TockenProvider)
        {
            this._AuthService = AuthService;
            this._TockenProvider = TockenProvider;
        }

        //the get method to project the login view
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        //the post method of the login to perform the functionality of the login
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequest)
        {
            var response = await _AuthService.Loginasync(loginRequest);

            if (response.IsSuccess == true)
            {
                var loginresponsedto = JsonConvert.DeserializeObject<LoginResponseDTO>(response.Result.ToString());
                //setting the token in the cookie
                _TockenProvider.SetToken(loginresponsedto.Token);
                //calling the signin method to let the app know that the user is logedin and authinticated
                await SignInUser(loginresponsedto);
                TempData["success"] = "Login Successful";

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", response.Message);
                TempData["error"] = response.Message;
                return View();
            }
        }

        //the get method to project the register view
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            //the role of the user 
            //showin it as a dropdown list for the consumer to select
            var role = new List<SelectListItem>
            {
                new SelectListItem{Value = SD.RoleAdmin, Text = SD.RoleAdmin},
                new SelectListItem{Value = SD.RoleUser, Text = SD.RoleUser}
            };
            //the viewbag to hold the data from the controler to the view 
            ViewBag.Role = role;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDTO obj)
        {
            var respons = await _AuthService.Registerasync(obj);
            var roleresponse = new ResponsDTO();
            if (respons.Result != null && respons.IsSuccess)
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.RoleUser;
                }
                roleresponse = await _AuthService.AssignRole(obj);
                if (roleresponse.IsSuccess && roleresponse != null)
                {
                    TempData["success"] = "Registeration Successful";
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    TempData["error"] = roleresponse.Message;

                    return RedirectToAction(nameof(Register));
                }
            }
            // has to add the role to the viewbag to be able to show it in the view again if the consumer registered wrong and got redirected to the register view again
            var role = new List<SelectListItem>
            {
                new SelectListItem{Value = SD.RoleAdmin, Text = SD.RoleAdmin},
                new SelectListItem{Value = SD.RoleUser, Text = SD.RoleUser}
            };
            ViewBag.Role = role;
            TempData["error"] = respons.Message;
            return View(obj);
        }


        
        public async Task<IActionResult> Logout()
        {
            HttpContext.SignOutAsync();
            _TockenProvider.ClearToken();
            TempData["success"] = "Logout Successful";
            return RedirectToAction("Index", "Home");
        }


        //creatin the signin method to be used in the login method
        //so the app can know when the user is logedin and authinticated
        private async Task SignInUser(LoginResponseDTO loginresponsedto)
        {
            //usign nugget package identitymodel.tooken.jwt to get the custom claim types and the readtoken method 
            var handler = new JwtSecurityTokenHandler();

            //reading the token to get the content of it specificly the claims to be used to create the claimidentity variable for the claimprincipal
            var jwt = handler.ReadJwtToken(loginresponsedto.Token);

            //creating hte claimidentity variable to be used in the claimprincipal
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            //adding the claims to the claimidentity variable
            identity .AddClaim(new Claim (JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value)); 
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            //this claim is built in in the identity framwork and it must be added and assigned for the singin method tp work
            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            var principal = new ClaimsPrincipal(identity);

            //calling the builtin signin method to let the app know that the user is logedin and authinticated
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}

