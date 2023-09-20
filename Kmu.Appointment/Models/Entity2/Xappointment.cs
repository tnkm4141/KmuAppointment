using System;
using System.Collections.Generic;

#nullable disable

namespace Kmu.Appointment.Models.Entity2
{
    public partial class Xappointment
    {
        public int Id { get; set; }
        public string Gsm { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public byte Status { get; set; }
        public string FullName { get; set; }
        public string UserIdentifer { get; set; }
        public int PolyclinicId { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
