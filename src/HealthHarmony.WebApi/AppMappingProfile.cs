using AutoMapper;
using HealthHarmony.Domain.Entities;
using HealthHarmony.Models;

namespace HealthHarmony;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Record, RecordReqeustModel>().ReverseMap();
        
        CreateMap<Record, RecordResponseModel>().ReverseMap();
    }
}