using _1_API.Response;
using Infraestructure;
using AutoMapper;
using Domain;

namespace _1_API.Mapper;

public class ModelsToResponse : Profile
{
    public ModelsToResponse()
    {
        CreateMap<Tutorial, TutorialResponse>();
        CreateMap<Section, SectionResponse>();
    }
}