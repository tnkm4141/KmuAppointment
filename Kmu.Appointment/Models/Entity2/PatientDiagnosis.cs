using System;
using System.Collections.Generic;

#nullable disable

namespace Kmu.Appointment.Models.Entity2
{
    public partial class PatientDiagnosis
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string Description { get; set; }
        public string Gsm { get; set; }
    }
}
