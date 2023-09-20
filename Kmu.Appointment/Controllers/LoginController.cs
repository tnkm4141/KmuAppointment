using Kmu.Appointment.Models.Entity2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kmu.Appointment.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(DbAppointmentContext context):base(context)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
       [HttpPost]
        public async Task <IActionResult> Index(SysUser user)
        {
            var datavalue1 = Context.SysUsers.SingleOrDefault(x=>x.Email==user.Email && x.Password==user.Password && x.Role== ((byte)Kmu.Appointment.Models.Enums.Role.Admin));
            var datavalue2 = Context.SysUsers.SingleOrDefault(x=>x.Email==user.Email && x.Password==user.Password && x.Role== ((byte)Kmu.Appointment.Models.Enums.Role.Doctor));
            if (datavalue1 != null )//Admin
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.Email),
                    new Claim(ClaimTypes.Role, ((byte)Kmu.Appointment.Models.Enums.Role.Admin).ToString())
                };
                var useridentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);

                //Gelen kullanıcının rolüne göre gideceiği sayfanın URli verilir.

                //return RedirectToAction("Index", "Profile");

                return RedirectToAction("Index", "DoctorList");
               

            }
           else if(datavalue2 != null)//dr
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.Email),
                    new Claim(ClaimTypes.Role,((byte)Kmu.Appointment.Models.Enums.Role.Doctor).ToString())
                };
                var useridentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);

                //Gelen kullanıcının rolüne göre gideceiği sayfanın URli verilir.

              // return RedirectToAction("Index", "DoctorList");


                 return RedirectToAction("Index", "Profile");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }


    }
}
