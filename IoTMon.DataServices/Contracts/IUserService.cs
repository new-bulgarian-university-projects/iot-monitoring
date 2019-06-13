using IoTMon.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.DataServices.Contracts
{
    public interface IUserService
    {
        UserDto Authenticate(LoginDto loginData);
        UserDto Create(RegisterDto registerData);
    }
}
