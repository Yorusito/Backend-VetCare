using VetCare.API.Shared.Domain.Services.Communication;
using VetCare.API.Faq.Domain.Models;

namespace VetCare.API.Faq.Resources;

public class QuestionResource
{
    public QuestionResource(string message) : base(message)
    {
        
    }

    public QuestionResource(Question resource) : base(resource)
    {
        
    }
}