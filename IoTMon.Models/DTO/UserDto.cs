using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        }
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

    }
}
