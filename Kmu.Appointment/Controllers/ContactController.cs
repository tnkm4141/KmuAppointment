using DevExtreme.AspNet.Mvc.Builders;
using Kmu.Appointment.Models.Entity2;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kmu.Appointment.Controllers
{
    public class ContactController : BaseController
    {
        
        public ContactController(DbAppointmentContext context) : base(context)
        {
           
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Contact c)
        {
            Context.Contacts.Add(c);
            Context.SaveChanges();

            return RedirectToAction("Index","Home");
        }
    }
}
