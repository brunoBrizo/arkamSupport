using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArkamSupportLibrary.Models;
using ArkanSupportLibray.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ArkamSupport.Controllers
{
    [EnableCors()]
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmailService _service;

        public EmailController(IMapper mapper, IEmailService _service)
        {
            this._service = _service;
            this._mapper = mapper;
        }

        [DisableCors]
        [HttpPost]
        public async Task<IActionResult> Send([FromBody] Email email)
        {
            try
            {
                await _service.sendEmail(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("hola");
        }


    }
}
