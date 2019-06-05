using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IoTMon.Models
{
    public class User
    {
        private ICollection<Device> devices;

        public User()
        {
            this.devices = new HashSet<Device>();
        }

        [Key]
        public Guid Id { get; set; }

        public ICollection<Device> Devices
        {
            get => this.devices;
            set => this.devices = value;
        }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}
