using Kmu.Appointment.Models.Entity2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

namespace Kmu.Appointment.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {

        public ProfileController(DbAppointmentContext context):base(context)
        {
        }
       
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult AdminNavbarPartial()
        {
            return PartialView();
        }
        public PartialViewResult DoctorsNavbarPartial()
        {
            return PartialView();
        }

    }
}
