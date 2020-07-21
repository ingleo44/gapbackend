using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.model
{
    public class Patient : Person
    {
        [Key] public int id { get; set; }
        public string insuranceCompany { get; set; }
        public bool active { get; set; } = true;
        public ICollection<Appointment> appointments { get; set; }
    }
}
