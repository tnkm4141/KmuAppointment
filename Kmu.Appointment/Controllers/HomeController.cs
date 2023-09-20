using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kmu.Appointment.Controllers;
using Kmu.Appointment.Models.Entity2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kmu_Appointment.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(DbAppointmentContext context) : base(context)
        {

        }
        public IActionResult Index()
        {
            var value = Context.Doctors.ToList();
            return View(value);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View();
        }
    }
}
