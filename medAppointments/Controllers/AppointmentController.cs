using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Services;
using DAL.model;
using medAppointments.Converters;
using medAppointments.ViewModel;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace medAppointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentServices _AppointmentServices;

        public AppointmentController(IAppointmentServices AppointmentServices)
        {
            _AppointmentServices = AppointmentServices;
        }


        [HttpGet()]
        [EnableQuery]
        public IActionResult GetAll()
        {
            try
            {
                var result = _AppointmentServices.GetAll();
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
                var result = _AppointmentServices.GetById(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AppointmentViewModel obj)
        {
            try
            {
                var newObject = ViewModelConverter.ConvertViewModel<AppointmentViewModel, Appointment>(obj);
                var result = await _AppointmentServices.Add(newObject);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] AppointmentViewModel obj)
        {
            try
            {
                var newObject = ViewModelConverter.ConvertViewModel<AppointmentViewModel, Appointment>(obj);
                var result = await _AppointmentServices.Update(newObject);
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
                await _AppointmentServices.Delete(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}