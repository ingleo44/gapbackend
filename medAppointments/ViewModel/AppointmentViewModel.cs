using System;

namespace medAppointments.ViewModel
{
    public class AppointmentViewModel
    {
        public int id { get; set; }
        public DateTime appointmentDate { get; set; }
        public int patientId { get; set; }
        public int appointmentTypeId { get; set; }
    }
}