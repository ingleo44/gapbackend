using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.model
{
    public class AppointmentType
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public bool active { get; set; } = true;
        public ICollection<Appointment> appointments { get; set; }
    }
}