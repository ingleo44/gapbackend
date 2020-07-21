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
    public class AppointmentTypeController : ControllerBase
    {
        private readonly IAppointmentTypeServices _AppointmentTypeServices;

        public AppointmentTypeController(IAppointmentTypeServices AppointmentTypeServices)
        {
            _AppointmentTypeServices = AppointmentTypeServices;
        }

        [EnableQuery]
        [HttpGet()]
        public IActionResult GetAll()
        {
            try
            {
                var result = _AppointmentTypeServices.GetAll();
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
                var result = _AppointmentTypeServices.GetById(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AppointmentTypeViewModel obj)
        {
            try
            {
                var newObject = ViewModelConverter.ConvertViewModel<AppointmentTypeViewModel, AppointmentType>(obj);
                var result = await _AppointmentTypeServices.Add(newObject);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] AppointmentTypeViewModel obj)
        {
            try
            {
                var newObject = ViewModelConverter.ConvertViewModel<AppointmentTypeViewModel, AppointmentType>(obj);
                var result = await _AppointmentTypeServices.Update(newObject);
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
                await _AppointmentTypeServices.Delete(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}