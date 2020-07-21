using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.model;
using EFCoreCommon.Model;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{

    public interface IAppointmentTypeRepository
    {
        AppointmentType GetById(int id);
        IQueryable<AppointmentType> GetAll();
        Task<AppointmentType> Add(AppointmentType entity);
        Task<AppointmentType> Update(AppointmentType entity);
        Task<bool> Delete(int id);
    }

    public class AppointmentTypeRepository : GenericRepository<AppointmentType>, IAppointmentTypeRepository
    {

        #region Class Declarations

        public AppointmentTypeRepository(MedAppointmentsContext dbContext) : base(dbContext)
        {
        }

        #endregion Class Declarations

        public AppointmentType GetById(int id)
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

        public IQueryable<AppointmentType> GetAll()
        {
            try
            {

                var result = Query().Include(q=>q.appointments);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex);
            }
        }


        public async Task<AppointmentType> Add(AppointmentType entity)
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

        public async Task<AppointmentType> Update(AppointmentType entity)
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
