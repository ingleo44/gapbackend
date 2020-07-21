using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DAL.model;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace Business.Services
{

    public interface IAppointmentServices
    {
        Task<Appointment> Add(Appointment entity);
        Task<Appointment> Update(Appointment entity);
        Task<bool> Delete(int id);
        Appointment GetById(int id);
        IEnumerable<Appointment> GetAll();
        Task<ICollection<Appointment>> GetAllFull();
    }

    public class AppointmentServices : IAppointmentServices
    {


        private readonly IAppointmentRepository _AppointmentRepository;


        public AppointmentServices(IAppointmentRepository AppointmentRepository)
        {
            _AppointmentRepository = AppointmentRepository;
        }

        public async Task<Appointment> Add(Appointment entity)
        {


            // we will check that there is no any appointment for the same day for this patient
            var appointmentsInTheSameDay = _AppointmentRepository.GetAll().FirstOrDefault(q => q.patientId == entity.patientId && q.appointmentDate.Date == entity.appointmentDate.Date);
            try
            {
                if (appointmentsInTheSameDay != null)
                {
                    throw new ValidationException("There is already an appointment for this patient this day");
                }

                if (entity.appointmentDate <= DateTime.Now)
                {
                    throw new ValidationException("The date of the appointment is before current date");
                }
                var bOpDoneSuccessfully = await _AppointmentRepository.Add(entity);
                return bOpDoneSuccessfully;
            }
            catch (Exception ex) when (ex.GetType().ToString() != "System.ComponentModel.DataAnnotations.ValidationException")
            {
                throw new Exception("BusinessLogic:AppointmentBusiness::InsertAppointment::Error occured.", ex);
            }
        }



        public async Task<Appointment> Update(Appointment entity)
        {
            try
            {


                var bOpDoneSuccessfully = await _AppointmentRepository.Update(entity);
                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {

                throw new Exception("BusinessLogic:AppointmentBusiness::UpdateAppointment::Error occured.", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var Appointment = _AppointmentRepository.GetById(id);
                if (Appointment.appointmentDate.AddDays(-1) < DateTime.Now) 
                {
                    throw new ValidationException("You can only cancel appointments before 24 hours of its scheduled date");
                }
                Appointment.active = false;
                await _AppointmentRepository.Update(Appointment);
                return true;
            }
            catch (Exception ex) when (ex.GetType().ToString() != "System.ComponentModel.DataAnnotations.ValidationException")
            {

                throw new Exception("BusinessLogic:AppointmentBusiness::DeleteAppointmentById::Error occured.", ex);
            }
        }

        public Appointment GetById(int id)
        {
            try
            {

                var result = _AppointmentRepository.GetById(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:AppointmentBusiness::SelectAppointmentById::Error occured.", ex);
            }
        }


        public IEnumerable<Appointment> GetAll()
        {


            try
            {
                var entities = _AppointmentRepository.GetAll().ToList();
                return entities;
            }
            catch (Exception ex)
            {

                throw new Exception("BusinessLogic:AppointmentBusiness::GetAllAppointment::Error occured.", ex);
            }
        }

        public async Task<ICollection<Appointment>> GetAllFull()
        {
            try
            {
                var entities = await _AppointmentRepository.GetAll().ToListAsync();
                return entities;
            }
            catch (Exception ex)
            {

                throw new Exception("BusinessLogic:AppointmentBusiness::GetAllFullAppointment::Error occured.", ex);
            }
        }
    }
}
