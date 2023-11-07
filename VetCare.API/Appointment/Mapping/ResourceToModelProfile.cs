using AutoMapper;
using VetCare.API.Appointment.Domain.Models;
using VetCare.API.Appointment.Resources;

namespace VetCare.API.Appointment.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SavePetResource, Pet>();
        CreateMap<SavePrescriptionResource, Prescription>();
    }
}