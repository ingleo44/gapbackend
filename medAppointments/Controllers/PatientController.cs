using System.Threading.Tasks;
using Business.Services;
using DAL.model;
using medAppointments.Converters;
using medAppointments.ViewModel;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace medAppointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IPatientServices _PatientServices;

        public PatientController(IPatientServices PatientServices)
        {
            _PatientServices = PatientServices;
        }


        [HttpGet()]
        [EnableQuery]
        public IActionResult GetAll()
        {
            try
            {
                var result = _PatientServices.GetAll();
                return new ObjectResult(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{id}")]
        public IActionResult GetByIdAsync(int id)
        {
            try
            {
                var result = _PatientServices.GetById(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Creates a Patient.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>   
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatientViewModel obj)
        {
            try
            {
                var newObject = ViewModelConverter.ConvertViewModel<PatientViewModel,Patient>(obj);
                newObject.firstName = obj.firstName;
                var result = await _PatientServices.Add(newObject);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

     
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] PatientViewModel obj)
        {
            try
            {
                var newObject = ViewModelConverter.ConvertViewModel<PatientViewModel, Patient>(obj);
                var result = await _PatientServices.Update(newObject);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _PatientServices.Delete(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}