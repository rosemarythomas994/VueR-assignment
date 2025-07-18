using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Revv.cars.Domain.Model;
using Revv.cars.Shared.Commands;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Revv.cars.Domain
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            CreateMap<Car, Shared.Car>().ReverseMap();
            CreateMap<Car, CreateCarCommandResponse>();
        }
    }
}
