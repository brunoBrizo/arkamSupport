using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ArkamSupportLibrary.Models;

namespace ArkanSupportLibray.Interfaces
{
    public interface IEmailService
    {
        Task sendEmail(Email email);
    }
}
