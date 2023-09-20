using System;
using System.Collections.Generic;

#nullable disable

namespace Kmu.Appointment.Models.Entity2
{
    public partial class Appointment
    {
        public int Id { get; set; }
        public int? Patient { get; set; }
        public int? Doctor { get; set; }
        public DateTime? Date { get; set; }

        public virtual Patient PatientNavigation { get; set; }
    }
}
