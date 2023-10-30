using VetCare.API.Appointment.Domain.Models;
using VetCare.API.Shared.Domain.Services.Communication;

namespace VetCare.API.Appointment.Domain.Services.Communication;

public class PetResponse : BaseResponse<Pet>
{
    public PetResponse(string message) : base(message)
    {
    }

    public PetResponse(Pet resource) : base(resource)
    {
    }
}