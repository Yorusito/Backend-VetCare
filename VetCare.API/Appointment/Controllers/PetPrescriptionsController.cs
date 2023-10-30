using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VetCare.API.Appointment.Domain.Models;
using VetCare.API.Appointment.Domain.Services;
using VetCare.API.Appointment.Resources;

namespace VetCare.API.Appointment.Controllers;

[ApiController]
[Route("/api/v1/pets/{petId}/prescriptions")]
public class PetPrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;
    private readonly IMapper _mapper;

    public PetPrescriptionsController(IPrescriptionService prescriptionService, IMapper mapper)
    {
        _prescriptionService = prescriptionService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<PrescriptionResource>> GetAllByCategoryIdAsync(int petId)
    {
        var prescriptions = await _prescriptionService.ListByCategoryIdAsync(petId);

        var resources = _mapper.Map<IEnumerable<Prescription>, IEnumerable<PrescriptionResource>>(prescriptions);

        return resources;
    }
}