using AutoMapper;
using Manager.API.ViewModels;
using Manager.API.ViewModes;
using Manager.Domain.Entity;
using Manager.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Manager.API.Configuration
{
 
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<User, UserDTO>().ReverseMap();
                config.CreateMap<CreateUserViewModel, UserDTO>().ReverseMap();
                config.CreateMap<UpdateUserViewModel, UserDTO>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
