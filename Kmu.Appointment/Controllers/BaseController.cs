using Kmu.Appointment.Models.Entity2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kmu.Appointment.Controllers
{
    public class BaseController : Controller
    {
        protected DbAppointmentContext Context;
        public BaseController(DbAppointmentContext context)
        {
            Context = context;
        }
    }
}
