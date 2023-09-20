using System;
using System.Collections.Generic;

#nullable disable

namespace Kmu.Appointment.Models.Entity2
{
    public partial class Polyclinic
    {
        public Polyclinic()
        {
            Doctors = new HashSet<Doctor>();
            Xdoctors = new HashSet<Xdoctor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Xdoctor> Xdoctors { get; set; }
    }
}
