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
        public IAuthService _AuthService { get; }

        public AuthController(IAuthService AuthService)
        {
            this._AuthService = AuthService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequest)
        {
            var response = await _AuthService.Loginasync(loginRequest);

            if (response.IsSuccess == true)
            {
                var loginresponsedto = JsonConvert.DeserializeObject<LoginResponseDTO>(response.Result.ToString());
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
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var role = new List<SelectListItem>
            {
                new SelectListItem{Value = SD.RoleAdmin, Text = SD.RoleAdmin},
                new SelectListItem{Value = SD.RoleUser, Text = SD.RoleUser}
            };
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

