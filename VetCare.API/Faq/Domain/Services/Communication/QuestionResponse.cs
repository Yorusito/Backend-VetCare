using VetCare.API.Shared.Domain.Services.Communication;
using VetCare.API.Faq.Domain.Models;

namespace VetCare.API.Faq.Domain.Services.Communication;

public class QuestionResponse
{
    public QuestionResponse(string message) : base(message)
    {
        
    }

    public QuestionResponse(Question resource) : base(resource)
    {
        
    }
}