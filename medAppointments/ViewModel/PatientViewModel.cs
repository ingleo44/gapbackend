using System;

namespace medAppointments.ViewModel
{
    public class PatientViewModel
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthDate { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string phone { get; set; }
        public string insuranceCompany { get; set; }
    }
}