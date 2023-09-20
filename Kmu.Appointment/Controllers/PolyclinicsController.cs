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
    [Route("[controller]/[action]")]
    public class PolyclinicsController : BaseController
    {

        public PolyclinicsController(DbAppointmentContext context):base(context) {
        }

        public IActionResult Polyclinics()
        {
            var value = Context.Polyclinics.ToList();
            return View(value);
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var polyclinics = Context.Polyclinics.Select(i => new {
                i.Id,
                i.Name,
                i.Description
            });

           

            return Json(await DataSourceLoader.LoadAsync(polyclinics, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Polyclinic();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = Context.Polyclinics.Add(model);
            await Context.SaveChangesAsync();

            return Json(new { result.Entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await Context.Polyclinics.FirstOrDefaultAsync(item => item.Id == key);
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
            var model = await Context.Polyclinics.FirstOrDefaultAsync(item => item.Id == key);

            Context.Polyclinics.Remove(model);
            await Context.SaveChangesAsync();
        }


        private void PopulateModel(Polyclinic model, IDictionary values) {
            string ID = nameof(Polyclinic.Id);
            string NAME = nameof(Polyclinic.Name);
            string DESCRİPTİON = nameof(Polyclinic.Description);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(NAME)) {
                model.Name = Convert.ToString(values[NAME]);
            }

            if(values.Contains(DESCRİPTİON)) {
                model.Description = Convert.ToString(values[DESCRİPTİON]);
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