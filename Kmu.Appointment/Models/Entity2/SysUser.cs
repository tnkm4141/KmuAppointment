using System;
using System.Collections.Generic;

#nullable disable

namespace Kmu.Appointment.Models.Entity2
{
    public partial class SysUser
    {
        public SysUser()
        {
            Xdoctors = new HashSet<Xdoctor>();
        }

        public int Id { get; set; }
        public string Gsm { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public byte Role { get; set; }

        public virtual ICollection<Xdoctor> Xdoctors { get; set; }
    }
}
