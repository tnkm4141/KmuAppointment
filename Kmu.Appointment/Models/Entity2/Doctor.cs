using System;
using System.Collections.Generic;

#nullable disable

namespace Kmu.Appointment.Models.Entity2
{
    public partial class Doctor
    {
        public Doctor()
        {
            Patients = new HashSet<Patient>();
            Xappointments = new HashSet<Xappointment>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string NameSurname { get; set; }
        public string Tc { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public int? Polyclinic { get; set; }
        public string Password { get; set; }

        public virtual Polyclinic PolyclinicNavigation { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Xappointment> Xappointments { get; set; }
    }
}
