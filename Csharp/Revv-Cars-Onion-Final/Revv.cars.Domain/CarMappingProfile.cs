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
            CreateMap<Car, Shared.Car>().ReverseMap(); // Bi-directional
                                                       // This creates two mappings:
                                                       //Domain.Model.Car → Shared.Car
                                                       //Shared.Car → Domain.Model.Car
                                                       //var shared = _mapper.Map<Shared.Car>(domainCar);   // ✅ domain → shared
                                                       // var domain = _mapper.Map<Car>(sharedCar);          // ✅ shared → domain



            /*This maps Domain.Car directly to your CreateCarCommandResponse DTO
             * Not strictly necessary for your question, but helpful if your response is separate from Shared.Car
             * */
            CreateMap<Car, CreateCarCommandResponse>();// Extra: for domain → response mapping
            CreateMap<CreateCarCommandRequest, Shared.Car>()
    .ForMember(dest => dest.Id, opt => opt.Ignore())
    .ForMember(dest => dest.Image, opt => opt.Ignore())
    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
    .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")));

        }
    }
}
