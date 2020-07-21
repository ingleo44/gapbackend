using DAL.model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface IPatientServices
    {
        Task<Patient> Add(Patient entity);
        Task<Patient> Update(Patient entity);
        Task<bool> Delete(int id);
        Patient GetById(int id);
        IEnumerable<Patient> GetAll();
    }

    public class PatientServices : IPatientServices
    {
      

        private readonly IPatientRepository _PatientRepository;


        public PatientServices(IPatientRepository PatientRepository)
        {
            _PatientRepository = PatientRepository;
        }

        public async Task<Patient> Add(Patient entity)
        {
            try
            {

                var bOpDoneSuccessfully = await _PatientRepository.Add(entity);
                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {

                throw new Exception("BusinessLogic:PatientBusiness::InsertPatient::Error occured.", ex);
            }
        }



        public async Task<Patient> Update(Patient entity)
        {
            try
            {


                var bOpDoneSuccessfully = await _PatientRepository.Update(entity);
                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {

                throw new Exception("BusinessLogic:PatientBusiness::UpdatePatient::Error occured.", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var patient = _PatientRepository.GetById(id);
                patient.active = false;
                await _PatientRepository.Update(patient);
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("BusinessLogic:PatientBusiness::DeletePatientById::Error occured.", ex);
            }
        }

        public Patient GetById(int id)
        {
            try
            {

                var result = _PatientRepository.GetById(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:PatientBusiness::SelectPatientById::Error occured.", ex);
            }
        }


        public IEnumerable<Patient> GetAll()
        {


            try
            {
                var entities = _PatientRepository.GetAll().ToList();
                return entities;
            }
            catch (Exception ex)
            {

                throw new Exception("BusinessLogic:PatientBusiness::GetAllPatient::Error occured.", ex);
            }
        }

       
    }
}