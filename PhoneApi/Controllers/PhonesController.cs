using Microsoft.AspNetCore.Mvc;
using PhoneApi.Dto;
using PhoneApi.Models;
using PhoneApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApi.Controllers
{
    [Route("phones")]
    [ApiController]
    public class PhonesController : ControllerBase 
    {
        private readonly IPhoneRepository _phoneRepo;
        public PhonesController(IPhoneRepository phoneRepo)
        {
            _phoneRepo = phoneRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhones()
        {
            try
            {
                var phones = await _phoneRepo.GetPhones();
                return Ok(phones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "PhoneById")]
        public async Task<IActionResult> GetPhone(int id)
        {
            try
            {
                var phone = await _phoneRepo.GetPhone(id);
                if (phone == null)
                    return NotFound();
                return Ok(phone);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreatePhone(PhoneForCreationDto phone)
        {
             try
             {
                var createdPhone = await _phoneRepo.CreatePhone(phone);
                return CreatedAtRoute("CompanyById", new { id = createdPhone.Id }, createdPhone);
             }
                catch (Exception ex)
             {
                    //log error
                    return StatusCode(500, ex.Message);
             }   
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePhone(int id, PhoneForUpdateDto phone)
        {
            try
            {
                var dbPhone = await _phoneRepo.GetPhone(id);
                if (dbPhone == null)
                    return NotFound();
                await _phoneRepo.UpdatePhone(id, phone);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhone(int id)
        {
            try
            {
                var dbPhone = await _phoneRepo.GetPhone(id);
                if (dbPhone == null)
                    return NotFound();
                await _phoneRepo.DeletePhone(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

       


        }
}
