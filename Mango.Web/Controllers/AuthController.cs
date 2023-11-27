using Mango.Web.Models;
using Mango.Web.services.Iservices;
using Mango.Web.utilities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task <IActionResult> Login(LoginRequestDTO loginRequest)
        {
            var response = await _AuthService.Loginasync(loginRequest);
            return View ();

            //if (response.IsSuccess == true)
            //{
            //    TempData["success"] = "Login Successful";
            
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    TempData["error"] = response.Message;
            //    return View();
            //}
        }
        [HttpGet]
        public async Task<IActionResult> Register(RegistrationRequestDTO registerRequest)
        {
            var role = new List<SelectListItem>
            {
                new SelectListItem{Value = SD.RoleAdmin, Text = SD.RoleAdmin},
                new SelectListItem{Value = SD.RoleUser, Text = SD.RoleUser}
            };
            ViewBag.Role = role;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            return View();
        }
    }
}
