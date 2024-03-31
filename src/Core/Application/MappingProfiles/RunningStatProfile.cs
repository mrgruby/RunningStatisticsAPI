using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles
{
    public class RunningStatProfile : Profile
    {
        public RunningStatProfile()
        {
            CreateMap<RunningStatConvertedDto, RunningStatConverted>().ReverseMap();
        }
    }
}