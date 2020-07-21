using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Services;
using DAL.model;
using NUnit.Framework;
using FizzWare.NBuilder;
using Moq;
using Repository.Repositories;

namespace NUnitTestProject1
{
    [TestFixture]
    public class PatientServiceTest
    {

        private IList<Patient> _referrals;
        private Mock<IPatientRepository> _patientRepository;

        private Patient setPatientId(Patient patient)
        {
            patient.id = 525;
            return patient;
        }

        [SetUp]
        public void Setup()
        {
            _referrals = Builder<Patient>.CreateListOfSize(10)
                .All()
                .Build();

            _patientRepository = new Mock<IPatientRepository>();
            _patientRepository.Setup(x => x.GetAll()).Returns(_referrals.AsQueryable);
            _patientRepository.Setup(x => x.Add(It.IsAny<Patient>())).ReturnsAsync((Patient x) => setPatientId(x));
            _patientRepository.Setup(x => x.Update(It.IsAny<Patient>())).ReturnsAsync((Patient x) => x);
            _patientRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(true);
            _patientRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) =>_referrals.FirstOrDefault(q => q.id==id));




        }

        [Test]
        public void TestGetAll()
        {
            var patientService = new PatientServices(_patientRepository.Object);
            var result = patientService.GetAll();
            Assert.True(result.Count()==10);

        }


        [Test]
        public void TestGetById()
        {
            var patientService = new PatientServices(_patientRepository.Object);
            var result = patientService.GetById(1);
            Assert.NotNull(result);

        }

        [Test]
        public async Task TestAddTask()
        {
            var patientService = new PatientServices(_patientRepository.Object);

            var newPatient = Builder<Patient>.CreateNew()
                .With(q=>q.id=0)
                .Build();
            var result = await patientService.Add(newPatient);
            Assert.True(result.id == 525);

        }


        [Test]
        public async Task TestUpdateTask()
        {
            var patientService = new PatientServices(_patientRepository.Object);

            var newPatient = Builder<Patient>.CreateNew()
                .With(q => q.id = 1)
                .Build();
            var result = await patientService.Update(newPatient);
            Assert.AreEqual(newPatient,result);

        }

        [Test]
        public async Task TestDeleteTask()
        {
            var patientService = new PatientServices(_patientRepository.Object);
            var result = await patientService.Delete(5);
            Assert.True(result);

        }
    }
}