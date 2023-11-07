using AutoMapper;
using VetCare.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using VetCare.API.Appointment.Domain.Models;
using VetCare.API.Appointment.Domain.Services;
using VetCare.API.Appointment.Resources;

namespace VetCare.API.Appointment.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class PetsController : ControllerBase
{
    private readonly IPetService _petService;
    private readonly IMapper _mapper;
    

    public PetsController(IPetService petService, IMapper mapper)
    {
        _petService = petService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<PetResource>> GetAllAsync()
    {
        var categories = await _petService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Pet>, IEnumerable<PetResource>>(categories);

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SavePetResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var category = _mapper.Map<SavePetResource, Pet>(resource);

        var result = await _petService.SaveAsync(category);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Pet, PetResource>(result.Resource);

        return Ok(categoryResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SavePetResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var category = _mapper.Map<SavePetResource, Pet>(resource);
        var result = await _petService.UpdateAsync(id, category);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Pet, PetResource>(result.Resource);

        return Ok(categoryResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _petService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var categoryResource = _mapper.Map<Pet, PetResource>(result.Resource);

        return Ok(categoryResource);
    }
    
}