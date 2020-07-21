using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.model
{
    public class Appointment
    {
        [Key]
        public int id { get; set; }
        public DateTime appointmentDate { get; set; }
        public int patientId { get; set; }
        public Patient patient { get; set; }

        public int appointmentTypeId { get; set; }

        public AppointmentType appointmentType { get; set; }
        public bool active { get; set; } = true;
    }
}