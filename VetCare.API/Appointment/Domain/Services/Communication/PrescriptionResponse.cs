using VetCare.API.Appointment.Domain.Models;
using VetCare.API.Shared.Domain.Services.Communication;

namespace VetCare.API.Appointment.Domain.Services.Communication;

public class PrescriptionResponse : BaseResponse<Prescription>
{
    public PrescriptionResponse(string message) : base(message)
    {
    }

    public PrescriptionResponse(Prescription resource) : base(resource)
    {
    }
}