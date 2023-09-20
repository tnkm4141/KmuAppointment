using System;
using System.Collections.Generic;

#nullable disable

namespace Kmu.Appointment.Models.Entity2
{
    public partial class Xdoctor
    {
        public int Id { get; set; }
        public int SysUserId { get; set; }
        public int PolyclinicId { get; set; }
        public string Img { get; set; }

        public virtual Polyclinic Polyclinic { get; set; }
        public virtual SysUser SysUser { get; set; }
    }
}
