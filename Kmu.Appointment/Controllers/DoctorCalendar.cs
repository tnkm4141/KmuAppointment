using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Kmu.Appointment.Models.Entity2;
using Kmu_Appointment.Models;


namespace Kmu.Appointment.Controllers
{
    public class DoctorCalendar : BaseController
    {
        public DoctorCalendar(DbAppointmentContext context) : base(context)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        /*public IActionResult GroupOrientation()
        {
            return View(SampleData.Tasks);
        }*/
    }
}
