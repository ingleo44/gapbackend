using System;
using Microsoft.VisualBasic;

namespace DAL.model
{
    public abstract class Person
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthDate { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string phone { get; set; }


    }
}