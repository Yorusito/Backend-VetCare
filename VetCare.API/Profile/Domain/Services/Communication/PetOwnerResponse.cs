using VetCare.API.Profile.Domain.Models;
using VetCare.API.Shared.Domain.Services.Communication;

namespace VetCare.API.Profile.Domain.Services.Communication;

public class PetOwnerResponse : BaseResponse<PetOwner>
{
    public PetOwnerResponse(string message) : base(message)
    {
    }

    public PetOwnerResponse(PetOwner resource) : base(resource)
    {
    }
}