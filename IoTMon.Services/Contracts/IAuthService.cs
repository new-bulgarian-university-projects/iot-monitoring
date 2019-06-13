using IoTMon.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Services.Contracts
{
    public interface IAuthService
    {
        string GenerateToken(UserDto user);
    }
}
