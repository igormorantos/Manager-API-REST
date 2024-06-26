﻿using Bogus.DataSets;
using Bogus;
using Manager.Domain.Entity;
using Manager.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Tests.Fixtures
{
    public class UserFixture
    {
        public static User CreateValidUser()
        {
            return new User(
                name: new Name().FirstName(),
                email: new Internet().Email(),
                password: new Internet().Password());
        }

        public static List<User> CreateListValidUser(int limit = 5)
        {
            var list = new List<User>();

            for (int i = 0; i < limit; i++)
                list.Add(CreateValidUser());

            return list;
        }

        public static UserDTO CreateValidUserDTO(bool newId = false)
        {
            return new UserDTO
            {
                Id = newId ? new Randomizer().Guid() : new Guid(),
                Name = new Name().FirstName(),
                Email = new Internet().Email(),
                Password = new Internet().Password()
            };
        }

        public static UserDTO CreateInvalidUserDTO()
        {
            return new UserDTO
            {
                Id = new Guid("77ef71ea-c0f6-ee11-948a-e0d55ef28ad3"),
                Name = "",
                Email = "",
                Password = ""
            };
        }
    }
}
