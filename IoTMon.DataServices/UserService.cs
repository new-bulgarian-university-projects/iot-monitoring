using IoTMon.Data;
using IoTMon.DataServices.Contracts;
using IoTMon.Models;
using IoTMon.Models.DTO;
using IoTMon.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoTMon.DataServices
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;

        private const string DefaultRole = "Customer";

        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException("dbContext");
        }

        public UserDto Authenticate(LoginDto loginData)
        {
            if (loginData == null)
                return null;

            var user = dbContext.Users
                .Where(x => x.Email == loginData.Email)
                .SingleOrDefault();

            // check if email exists
            if (user == null)
                return null;

            // check if password is correct
            if (!AuthUtils.VerifyPasswordHash(loginData.Password, user.PasswordHashed, user.PasswordSalted))
                return null;

            // authentication successful
            return new UserDto(user);
        }

        public UserDto Create(RegisterDto userData)
        {
            // validation
            if (userData == null)
                throw new ArgumentNullException("Password is required");

            if (this.dbContext.Users.Any(x => x.Email == userData.Email))
                throw new ArgumentException("Email \"" + userData.Email + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            AuthUtils.CreatePasswordHash(userData.Password, out passwordHash, out passwordSalt);
            
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Email = userData.Email,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                PasswordHashed = passwordHash,
                PasswordSalted = passwordSalt
            };

            this.dbContext.Users.Add(user);
            this.dbContext.SaveChanges();

            return new UserDto(user);
        }
    }
}
