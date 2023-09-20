using System;
using System.Collections.Generic;

#nullable disable

namespace Kmu.Appointment.Models.Entity2
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
