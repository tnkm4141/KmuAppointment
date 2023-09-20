using System;
using System.Collections.Generic;

#nullable disable

namespace Kmu.Appointment.Models.Entity2
{
    public partial class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Tc { get; set; }
        public int? Doctor { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }

        public virtual Doctor DoctorNavigation { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
