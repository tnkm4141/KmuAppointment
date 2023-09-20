using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Kmu.Appointment.Models.Entity2;

namespace Kmu.Appointment.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DoctorProfileController : BaseController
    {

        public DoctorProfileController(DbAppointmentContext context) :base(context){
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var doctors = Context.Doctors.Where(i => i.Id == 1).Select(i => new {
                i.Id,
                i.Title,
                i.NameSurname,
                i.Tc,
                i.Telephone,
                i.Mail,
                i.Polyclinic,
                i.Password
            });

           

            return Json(await DataSourceLoader.LoadAsync(doctors, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Doctor();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = Context.Doctors.Add(model);
            await Context.SaveChangesAsync();

            return Json(new { result.Entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await Context.Doctors.FirstOrDefaultAsync(item => item.Id == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await Context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await Context.Doctors.FirstOrDefaultAsync(item => item.Id == key);

            Context.Doctors.Remove(model);
            await Context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> PolyclinicsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in Context.Polyclinics
                         orderby i.Name
                         select new {
                             Value = i.Id,
                             Text = i.Name
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Doctor model, IDictionary values) {
            string ID = nameof(Doctor.Id);
            string TİTLE = nameof(Doctor.Title);
            string NAME_SURNAME = nameof(Doctor.NameSurname);
            string TC = nameof(Doctor.Tc);
            string TELEPHONE = nameof(Doctor.Telephone);
            string MAİL = nameof(Doctor.Mail);
            string POLYCLİNİC = nameof(Doctor.Polyclinic);
            string PASSWORD = nameof(Doctor.Password);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(TİTLE)) {
                model.Title = Convert.ToString(values[TİTLE]);
            }

            if(values.Contains(NAME_SURNAME)) {
                model.NameSurname = Convert.ToString(values[NAME_SURNAME]);
            }

            if(values.Contains(TC)) {
                model.Tc = Convert.ToString(values[TC]);
            }

            if(values.Contains(TELEPHONE)) {
                model.Telephone = Convert.ToString(values[TELEPHONE]);
            }

            if(values.Contains(MAİL)) {
                model.Mail = Convert.ToString(values[MAİL]);
            }

            if(values.Contains(POLYCLİNİC)) {
                model.Polyclinic = values[POLYCLİNİC] != null ? Convert.ToInt32(values[POLYCLİNİC]) : (int?)null;
            }

            if(values.Contains(PASSWORD)) {
                model.Password = Convert.ToString(values[PASSWORD]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}