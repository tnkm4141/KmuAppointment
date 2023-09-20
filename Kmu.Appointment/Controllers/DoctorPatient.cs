using Kmu.Appointment.Models.Entity2;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections;
using System.Globalization;

namespace Kmu.Appointment.Controllers
{
    [Route("[controller]/[action]")]
    public class DoctorPatient : BaseController
    {
        public DoctorPatient(DbAppointmentContext context) : base(context)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions,Doctor doctor)
        {
            var patients = Context.Patients.Where(i=>i.Doctor== doctor.Id).Select(i => new {
                i.Id,
                i.NameSurname,
                i.Tc,
                i.Doctor,
                i.Mail,
                i.Telephone 
            });

           

            return Json(await DataSourceLoader.LoadAsync(patients, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var model = new Patient();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = Context.Patients.Add(model);
            await Context.SaveChangesAsync();

            return Json(new { result.Entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {
            var model = await Context.Patients.FirstOrDefaultAsync(item => item.Id == key);
            if (model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await Context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key)
        {
            var model = await Context.Patients.FirstOrDefaultAsync(item => item.Id == key);

            Context.Patients.Remove(model);
            await Context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> DoctorsLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in Context.Doctors
                         orderby i.Title
                         select new
                         {
                             Value = i.Id,
                             Text = i.NameSurname
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Patient model, IDictionary values)
        {
            string ID = nameof(Patient.Id);
            string NAME_SURNAME = nameof(Patient.NameSurname);
            string TC = nameof(Patient.Tc);
            string DOCTOR = nameof(Patient.Doctor);
            string MAİL = nameof(Patient.Mail);
            string TELEPHONE = nameof(Patient.Telephone);

            if (values.Contains(ID))
            {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if (values.Contains(NAME_SURNAME))
            {
                model.NameSurname = Convert.ToString(values[NAME_SURNAME]);
            }

            if (values.Contains(TC))
            {
                model.Tc = Convert.ToString(values[TC]);
            }

            if (values.Contains(DOCTOR))
            {
                model.Doctor = values[DOCTOR] != null ? Convert.ToInt32(values[DOCTOR]) : (int?)null;
            }

            if (values.Contains(MAİL))
            {
                model.Mail = Convert.ToString(values[MAİL]);
            }

            if (values.Contains(TELEPHONE))
            {
                model.Telephone = Convert.ToString(values[TELEPHONE]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach (var entry in modelState)
            {
                foreach (var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}

