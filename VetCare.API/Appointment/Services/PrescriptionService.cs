using VetCare.API.Appointment.Domain.Models;
using VetCare.API.Appointment.Domain.Repositories;
using VetCare.API.Appointment.Domain.Services;
using VetCare.API.Appointment.Domain.Services.Communication;

namespace VetCare.API.Appointment.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPetRepository _petRepository;

    public PrescriptionService(IPrescriptionRepository prescriptionRepository, IUnitOfWork unitOfWork, IPetRepository petRepository)
    {
        _prescriptionRepository = prescriptionRepository;
        _unitOfWork = unitOfWork;
        _petRepository = petRepository;
    }

    public async Task<IEnumerable<Prescription>> ListAsync()
    {
        return await _prescriptionRepository.ListAsync();
    }

    public async Task<IEnumerable<Prescription>> ListByCategoryIdAsync(int categoryId)
    {
        return await _prescriptionRepository.FindByCategoryIdAsync(categoryId);
    }

    public async Task<PrescriptionResponse> SaveAsync(Prescription prescription)
    {
        // Validate CategoryId

        var existingCategory = await _petRepository.FindByIdAsync(prescription.CategoryId);

        if (existingCategory == null)
            return new PrescriptionResponse("Invalid Category");
        
        // Validate Title

        var existingTutorialWithTitle = await _prescriptionRepository.FindByTitleAsync(prescription.Title);

        if (existingTutorialWithTitle != null)
            return new PrescriptionResponse("Tutorial title already exists.");

        try
        {
            // Add Tutorial
            await _prescriptionRepository.AddAsync(prescription);
            
            // Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            // Return response
            return new PrescriptionResponse(prescription);

        }
        catch (Exception e)
        {
            // Error Handling
            return new PrescriptionResponse($"An error occurred while saving the tutorial: {e.Message}");
        }

        
    }

    public async Task<PrescriptionResponse> UpdateAsync(int prescriptionId, Prescription prescription)
    {
        var existingTutorial = await _prescriptionRepository.FindByIdAsync(prescriptionId);
        
        // Validate Tutorial

        if (existingTutorial == null)
            return new PrescriptionResponse("Tutorial not found.");

        // Validate CategoryId

        var existingCategory = await _petRepository.FindByIdAsync(prescription.CategoryId);

        if (existingCategory == null)
            return new PrescriptionResponse("Invalid Category");
        
        // Validate Title

        var existingTutorialWithTitle = await _prescriptionRepository.FindByTitleAsync(prescription.Title);

        if (existingTutorialWithTitle != null && existingTutorialWithTitle.Id != existingTutorial.Id)
            return new PrescriptionResponse("Tutorial title already exists.");
        
        // Modify Fields
        existingTutorial.Title = prescription.Title;
        existingTutorial.Description = prescription.Description;
        existingTutorial.Published = prescription.Published;
        try
        {
            _prescriptionRepository.Update(existingTutorial);
            await _unitOfWork.CompleteAsync();

            return new PrescriptionResponse(existingTutorial);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new PrescriptionResponse($"An error occurred while updating the tutorial: {e.Message}");
        }

    }

    public async Task<PrescriptionResponse> DeleteAsync(int prescriptionId)
    {
        var existingTutorial = await _prescriptionRepository.FindByIdAsync(prescriptionId);
        
        // Validate Tutorial

        if (existingTutorial == null)
            return new PrescriptionResponse("Tutorial not found.");
        
        try
        {
            _prescriptionRepository.Remove(existingTutorial);
            await _unitOfWork.CompleteAsync();

            return new PrescriptionResponse(existingTutorial);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new PrescriptionResponse($"An error occurred while deleting the tutorial: {e.Message}");
        }

    }
}