using System;
using System.Collections.Generic;

#nullable disable

namespace Kmu.Appointment.Models.Entity2
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }
        public string Message { get; set; }
    }
}
