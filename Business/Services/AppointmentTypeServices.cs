using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.model;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace Business.Services
{
    public interface IAppointmentTypeServices
    {
        Task<AppointmentType> Add(AppointmentType entity);
        Task<AppointmentType> Update(AppointmentType entity);
        Task<bool> Delete(int id);
        AppointmentType GetById(int id);
        IEnumerable<AppointmentType> GetAll();
        Task<ICollection<AppointmentType>> GetAllFull();
    }

    public class AppointmentTypeServices : IAppointmentTypeServices
    {


        private readonly IAppointmentTypeRepository _AppointmentTypeRepository;


        public AppointmentTypeServices(IAppointmentTypeRepository AppointmentTypeRepository)
        {
            _AppointmentTypeRepository = AppointmentTypeRepository;
        }

        public async Task<AppointmentType> Add(AppointmentType entity)
        {
            try
            {

                var bOpDoneSuccessfully = await _AppointmentTypeRepository.Add(entity);
                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {

                throw new Exception("BusinessLogic:AppointmentTypeBusiness::InsertAppointmentType::Error occured.", ex);
            }
        }



        public async Task<AppointmentType> Update(AppointmentType entity)
        {
            try
            {


                var bOpDoneSuccessfully = await _AppointmentTypeRepository.Update(entity);
                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {

                throw new Exception("BusinessLogic:AppointmentTypeBusiness::UpdateAppointmentType::Error occured.", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var AppointmentType = _AppointmentTypeRepository.GetById(id);
                AppointmentType.active = false;
                await _AppointmentTypeRepository.Update(AppointmentType);
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("BusinessLogic:AppointmentTypeBusiness::DeleteAppointmentTypeById::Error occured.", ex);
            }
        }

        public AppointmentType GetById(int id)
        {
            try
            {

                var result = _AppointmentTypeRepository.GetById(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:AppointmentTypeBusiness::SelectAppointmentTypeById::Error occured.", ex);
            }
        }


        public IEnumerable<AppointmentType> GetAll()
        {


            try
            {
                var entities = _AppointmentTypeRepository.GetAll().ToList();
                return entities;
            }
            catch (Exception ex)
            {

                throw new Exception("BusinessLogic:AppointmentTypeBusiness::GetAllAppointmentType::Error occured.", ex);
            }
        }

        public async Task<ICollection<AppointmentType>> GetAllFull()
        {
            try
            {
                var entities = await _AppointmentTypeRepository.GetAll().ToListAsync();
                return entities;
            }
            catch (Exception ex)
            {

                throw new Exception("BusinessLogic:AppointmentTypeBusiness::GetAllFullAppointmentType::Error occured.", ex);
            }
        }
    }
}