using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Business.Services;
using DAL.model;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using Repository.Repositories;

namespace NUnitTestProject1
{
    [TestFixture]
    public class AppointmentServiceTest
    {

        private IList<Appointment> _appointments;
        private Mock<IAppointmentRepository> _AppointmentRepository;

        private Appointment setAppointmentId(Appointment Appointment)
        {
            Appointment.id = 525;
            return Appointment;
        }

        [SetUp]
        public void Setup()
        {
            _appointments = Builder<Appointment>.CreateListOfSize(10)
                .All()
                .With(q =>q.patientId = 1)
                .Build();
            var currentDate = DateTime.Now;

            foreach (var appointment in _appointments)
            {
                currentDate = currentDate.AddDays(1);
                appointment.appointmentDate = currentDate;
            }

            _AppointmentRepository = new Mock<IAppointmentRepository>();
            _AppointmentRepository.Setup(x => x.GetAll()).Returns(_appointments.AsQueryable);
            _AppointmentRepository.Setup(x => x.Add(It.IsAny<Appointment>())).ReturnsAsync((Appointment x) => setAppointmentId(x));
            _AppointmentRepository.Setup(x => x.Update(It.IsAny<Appointment>())).ReturnsAsync((Appointment x) => x);
            _AppointmentRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(true);
            _AppointmentRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) => _appointments.FirstOrDefault(q => q.id == id));




        }

        [Test]
        public void TestGetAll()
        {
            var AppointmentService = new AppointmentServices(_AppointmentRepository.Object);
            var result = AppointmentService.GetAll();
            Assert.True(result.Count() == 10);

        }


        [Test]
        public void TestGetById()
        {
            var AppointmentService = new AppointmentServices(_AppointmentRepository.Object);
            var result = AppointmentService.GetById(1);
            Assert.NotNull(result);

        }

        [Test]
        public async Task TestAddTask()
        {
            var AppointmentService = new AppointmentServices(_AppointmentRepository.Object);
            var currentDate = DateTime.Now;
            var newAppointment = Builder<Appointment>.CreateNew()
                .With(q => q.id = 0)
                .With(q=>q.patientId=1)
                .With(q=>q.appointmentDate = currentDate.AddDays(25))
                .Build();
            var result = await AppointmentService.Add(newAppointment);
            Assert.True(result.id == 525);

        }

        [Test]
        public void TestAddTaskShouldThrowException_Appointmentinthesamedayexists()
        {
            var AppointmentService = new AppointmentServices(_AppointmentRepository.Object);
            var currentDate = DateTime.Now;
            var newAppointment = Builder<Appointment>.CreateNew()
                .With(q => q.id = 0)
                .With(q => q.patientId = 1)
                .With(q => q.appointmentDate = currentDate.AddDays(1))
                .Build();
            ;
            Assert.ThrowsAsync<ValidationException>(async () => await AppointmentService.Add(newAppointment));

        }

        [Test]
        public void TestAddTaskShouldThrowException_Appointmentdayinthepast()
        {
            var AppointmentService = new AppointmentServices(_AppointmentRepository.Object);
            var currentDate = DateTime.Now;
            var newAppointment = Builder<Appointment>.CreateNew()
                .With(q => q.id = 0)
                .With(q => q.patientId = 1)
                .With(q => q.appointmentDate = currentDate.AddDays(-5))
                .Build();
            ;
            Assert.ThrowsAsync<ValidationException>(async () => await AppointmentService.Add(newAppointment));

        }


        [Test]
        public async Task TestUpdateTask()
        {
            var AppointmentService = new AppointmentServices(_AppointmentRepository.Object);

            var newAppointment = Builder<Appointment>.CreateNew()
                .With(q => q.id = 1)
                .Build();
            var result = await AppointmentService.Update(newAppointment);
            Assert.AreEqual(newAppointment, result);

        }

        [Test]
        public async Task TestDeleteTask()
        {
            var AppointmentService = new AppointmentServices(_AppointmentRepository.Object);
            var result = await AppointmentService.Delete(5);
            Assert.True(result);

        }



        [Test]
        public async Task TestDeleteTaskShouldThrowException_cannotcancelappointmentoftoday()
        {
            var todayAppointment = Builder<Appointment>.CreateNew()
                .With(q => q.id = 25)
                .With(q => q.patientId = 1)
                .With(q => q.appointmentDate = DateTime.Now.AddMinutes(5))
                .Build();

            _AppointmentRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) => todayAppointment);

            var AppointmentService = new AppointmentServices(_AppointmentRepository.Object);
            Assert.ThrowsAsync<ValidationException>(async () => await AppointmentService.Delete(25));


        }
    }
}