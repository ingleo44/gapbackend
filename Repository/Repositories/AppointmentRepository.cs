using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.model;
using EFCoreCommon.Model;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public interface IAppointmentRepository
    {
        Appointment GetById(int id);
        IQueryable<Appointment> GetAll();
        Task<Appointment> Add(Appointment entity);
        Task<Appointment> Update(Appointment entity);
        Task<bool> Delete(int id);
    }

    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {

        #region Class Declarations

        public AppointmentRepository(MedAppointmentsContext dbContext) : base(dbContext)
        {
        }

        #endregion Class Declarations

        public Appointment GetById(int id)
        {

            try
            {
                var result = Get(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex);
            }
        }

        public IQueryable<Appointment> GetAll()
        {
            try
            {

                var result = Query().Include(q=>q.patient).Include(q=>q.appointmentType);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex);
            }
        }



        public async Task<Appointment> Add(Appointment entity)
        {
            try
            {
                var result = await InsertAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex);
            }
        }

        public async Task<Appointment> Update(Appointment entity)
        {
            try
            {
                var result = await UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                await DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex);
            }
        }

    }
}