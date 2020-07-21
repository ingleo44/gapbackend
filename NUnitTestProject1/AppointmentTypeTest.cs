using System.Collections.Generic;
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
    public class AppointmentTypeTest
    {
        private IList<AppointmentType> _referrals;
        private Mock<IAppointmentTypeRepository> _AppointmentTypeRepository;

        private AppointmentType setAppointmentTypeId(AppointmentType AppointmentType)
        {
            AppointmentType.id = 525;
            return AppointmentType;
        }

        [SetUp]
        public void Setup()
        {
            _referrals = Builder<AppointmentType>.CreateListOfSize(10)
                .All()
                .Build();

            _AppointmentTypeRepository = new Mock<IAppointmentTypeRepository>();
            _AppointmentTypeRepository.Setup(x => x.GetAll()).Returns(_referrals.AsQueryable);
            _AppointmentTypeRepository.Setup(x => x.Add(It.IsAny<AppointmentType>())).ReturnsAsync((AppointmentType x) => setAppointmentTypeId(x));
            _AppointmentTypeRepository.Setup(x => x.Update(It.IsAny<AppointmentType>())).ReturnsAsync((AppointmentType x) => x);
            _AppointmentTypeRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(true);
            _AppointmentTypeRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) => _referrals.FirstOrDefault(q => q.id == id));




        }

        [Test]
        public void TestGetAll()
        {
            var AppointmentTypeService = new AppointmentTypeServices(_AppointmentTypeRepository.Object);
            var result = AppointmentTypeService.GetAll();
            Assert.True(result.Count() == 10);

        }


        [Test]
        public void TestGetById()
        {
            var AppointmentTypeService = new AppointmentTypeServices(_AppointmentTypeRepository.Object);
            var result = AppointmentTypeService.GetById(1);
            Assert.NotNull(result);

        }

        [Test]
        public async Task TestAddTask()
        {
            var AppointmentTypeService = new AppointmentTypeServices(_AppointmentTypeRepository.Object);

            var newAppointmentType = Builder<AppointmentType>.CreateNew()
                .With(q => q.id = 0)
                .Build();
            var result = await AppointmentTypeService.Add(newAppointmentType);
            Assert.True(result.id == 525);

        }


        [Test]
        public async Task TestUpdateTask()
        {
            var AppointmentTypeService = new AppointmentTypeServices(_AppointmentTypeRepository.Object);

            var newAppointmentType = Builder<AppointmentType>.CreateNew()
                .With(q => q.id = 1)
                .Build();
            var result = await AppointmentTypeService.Update(newAppointmentType);
            Assert.AreEqual(newAppointmentType, result);

        }

        [Test]
        public async Task TestDeleteTask()
        {
            var AppointmentTypeService = new AppointmentTypeServices(_AppointmentTypeRepository.Object);
            var result = await AppointmentTypeService.Delete(5);
            Assert.True(result);

        }
    }
}
