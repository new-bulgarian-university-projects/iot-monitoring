using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace IoTMon.Models.DTO
{
    public class UserDto
    {
        public UserDto(User u)
        {
            this.Id = u.Id;
            this.FirstName = u.FirstName;
            this.LastName = u.LastName;
            this.Email = u.Email;

            this.Devices = u.Devices.Select(d => new DeviceDTO(d));
        }
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public IEnumerable<DeviceDTO> Devices { get; set; }

    }
}
