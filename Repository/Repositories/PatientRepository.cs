using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.model;
using EFCoreCommon.Model;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public interface IPatientRepository
    {
        Patient GetById(int id);
        IQueryable<Patient> GetAll();
        Task<Patient> Add(Patient entity);
        Task<Patient> Update(Patient entity);
        Task<bool> Delete(int id);
    }

    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {

        #region Class Declarations

        public PatientRepository(MedAppointmentsContext dbContext) : base(dbContext)
        {
        }

        #endregion Class Declarations

        public Patient GetById(int id)
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

        public IQueryable<Patient> GetAll()
        {
            try
            {

                var result = Query().Include(q=>q.appointments).ThenInclude(q=>q.appointmentType);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex);
            }
        }



        public async Task<Patient> Add(Patient entity)
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

        public async Task<Patient> Update(Patient entity)
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


