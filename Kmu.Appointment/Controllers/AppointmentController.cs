using Kmu.Appointment.Models;
using Kmu.Appointment.Models.Entity2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Kmu.Appointment.Controllers
{
    public class AppointmentController : BaseController
    {
        public AppointmentController(DbAppointmentContext context) 
            : base(context)
        {
           
        }
      
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddAppointment()
        {
        //    List<SelectListItem> doctors = (from x in Context.Doctors.ToList()
        //                                    select new SelectListItem
        //                                    {
        //                                        Text = x.NameSurname,
        //                                        Value = x.Id.ToString()
        //                                    }).ToList();
            List<SelectListItem> polyclics = (from x in Context.Polyclinics.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();
             //foreach (var item1 in Context.Polyclinics.ToList())
             //{
             //    polyclics.Add(new SelectListItem { Text = item1.Name, Value = item1.Id.ToString() });

             //}
             //foreach (var item2 in Context.Doctors.ToList())
             //{   
             //        doctors.Add(new SelectListItem { Text = item2.NameSurname, Value = item2.Id.ToString() });
             //}

             ViewBag.Polyclinic = polyclics;
            // ViewBag.Doctor = doctors;
             return View();
        
        }
       [HttpPost]
        public IActionResult AddAppointment(Xappointment? p)//*
        {
            Context.Xappointments.Add(p);
            
            Context.SaveChanges();
            
            return RedirectToAction("AddAppointment");
        }

       
        public JsonResult LoadDoc(int p)
        {
            //var docs = (from doc in Context.Doctors
            //            join pol in Context.Polyclinics on doc.Polyclinic equals pol.Id
            //            where doc.Polyclinic == p
            //            select new
            //            {
            //              Text = doc.NameSurname,
            //                Value = doc.Id.ToString()
            //            }).ToList();
           var docs = Context.Doctors.Where(z => z.Polyclinic == Convert.ToInt32(p)).ToList();
            //return  Json(new SelectList(docs,"Id", "NameSurname"));
            return Json(docs);
        }
        [HttpGet]
        public IActionResult CancelAppointment()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult CancelAppointment(int id)
        {
            var value = Context.Xappointments.Find(id);
            if (value != null)
            {
            Context.Xappointments.Remove(value);
            Context.SaveChanges();
            }
            else
            {
                ViewBag.ErorrMessge("Yanlış ID");
            }
           
            return RedirectToAction("CancelAppointment"); 
            //return View();
        }

    }
}
