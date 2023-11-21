using VetCare.API.Profile.Domain.Models;
using VetCare.API.Profile.Domain.Services.Communication;

namespace VetCare.API.Profile.Domain.Services;

public interface IPetOwnerService
{
    Task<IEnumerable<PetOwner>> ListAsync();
    Task<PetOwnerResponse> SaveAsync(PetOwner petOwner);
    Task<PetOwnerResponse> UpdateAsync(int id, PetOwner petOwner);
    Task<PetOwnerResponse> DeleteAsync(int id);
}