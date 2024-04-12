using AutoMapper;
using Manager.API.ViewModels;
using Manager.API.ViewModes;
using Manager.Domain.Entity;
using Manager.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Tests.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static IMapper GetConfiguration()
        {
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>()
                    .ReverseMap();

                cfg.CreateMap<CreateUserViewModel, UserDTO>()
                    .ReverseMap();

                cfg.CreateMap<UpdateUserViewModel, UserDTO>()
                    .ReverseMap();
            });

            return autoMapperConfig.CreateMapper();
        }
    }
}
