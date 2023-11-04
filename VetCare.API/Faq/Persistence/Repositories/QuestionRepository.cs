using Microsoft.EntityFrameworkCore;
using VetCare.API.Shared.Persistence.Contexts;
using VetCare.API.Shared.Persistence.Repositories;
using VetCare.API.Faq.Domain.Models;
using VetCare.API.Faq.Domain.Repositories;

namespace VetCare.API.Faq.Persistence.Repositories;

public class QuestionRepository
{
    public QuestionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Question>> ListAsync()
    {
        return await _context.Question
            .ToListAsync();
    }

    public async Task AddAsync(Question question)
    {
        await _context.Question.AddAsync(question);
    }

    public async Task<Question> FindByIdAsync(int questionId)
    {
        return await _context.Question
            .FirstOrDefaultAsync(p => p.Id == questionId);
    }

    public void Update(Question question)
    {
        _context.Question.Update(question);
    }

    public void Remove(Question question)
    {
        _context.Question.Remove(question);
    }
}