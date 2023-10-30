using VetCare.API.Appointment.Domain.Models;
using VetCare.API.Appointment.Domain.Repositories;
using VetCare.API.Appointment.Domain.Services;
using VetCare.API.Appointment.Domain.Services.Communication;

namespace VetCare.API.Appointment.Services;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PetService(IPetRepository petRepository, IUnitOfWork unitOfWork)
    {
        _petRepository = petRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Pet>> ListAsync()
    {
        return await _petRepository.ListAsync();
    }

    public async Task<PetResponse> SaveAsync(Pet pet)
    {
        try
        {
            await _petRepository.AddAsync(pet);
            await _unitOfWork.CompleteAsync();
            return new PetResponse(pet);
        }
        catch (Exception e)
        {
            return new PetResponse($"An error occurred while saving the category: {e.Message}");
        }
    }

    public async Task<PetResponse> UpdateAsync(int id, Pet pet)
    {
        var existingCategory = await _petRepository.FindByIdAsync(id);

        if (existingCategory == null)
            return new PetResponse("Category not found.");

        existingCategory.Name = pet.Name;
        existingCategory.Breed = pet.Breed;
        existingCategory.Weight = pet.Weight;
        existingCategory.Type = pet.Type;
        existingCategory.photoUrl = pet.photoUrl;
        existingCategory.Color = pet.Color;
        existingCategory.Date = pet.Date;
        

        try
        {
            _petRepository.Update(existingCategory);
            await _unitOfWork.CompleteAsync();

            return new PetResponse(existingCategory);
        }
        catch (Exception e)
        {
            return new PetResponse($"An error occurred while updating the category: {e.Message}");
        }
    }

    public async Task<PetResponse> DeleteAsync(int id)
    {
        var existingCategory = await _petRepository.FindByIdAsync(id);

        if (existingCategory == null)
            return new PetResponse("Category not found.");

        try
        {
            _petRepository.Remove(existingCategory);
            await _unitOfWork.CompleteAsync();

            return new PetResponse(existingCategory);
        }
        catch (Exception e)
        {
            // Do some logging stuff
            return new PetResponse($"An error occurred while deleting the category: {e.Message}");
        }
    }
}