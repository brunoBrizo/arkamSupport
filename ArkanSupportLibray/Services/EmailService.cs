using ArkamSupportLibrary.Models;
using ArkanSupportLibray.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArkamSupport.Services
{
    public class EmailService : IEmailService
    {
        public async Task sendEmail(Email email)
        {
            try
            {
                email.sendEmail();
            }
            catch (Exception e)
            {
                throw new Exception("Error enviando email | " + e.Message);
            }


        }
    }
}
