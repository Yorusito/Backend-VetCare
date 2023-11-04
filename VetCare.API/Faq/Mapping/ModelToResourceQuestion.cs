﻿using AutoMapper;
using VetCare.API.Faq.Domain.Models;
using VetCare.API.Faq.Domain.Services.Communication;
using VetCare.API.Faq.Resources;

namespace VetCare.API.Faq.Mapping;
    
public class ModelToResourceQuestion :Profile
{
    public ModelToResourceQuestion()
    {
        CreateMap<Question, QuestionResponse>();
    }
}