using Mango.Web.Model;
using Mango.Web.Models;
using Mango.Web.services.Iservices;
using Mango.Web.utilities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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
            return View(obj);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            return View();
        }
    }
}

