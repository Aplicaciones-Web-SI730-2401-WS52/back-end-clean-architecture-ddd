using Domain;
using Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure;

public class TutorialRepository : ITutorialRepository
{
    private readonly LearningCenterContext _learningCenterContext;

    public TutorialRepository(LearningCenterContext learningCenterContext)
    {
        _learningCenterContext = learningCenterContext;
    }

    public async Task<List<Tutorial>> GetAllAsync()
    {
        // COnecta BBDD MySQL
        var result = await _learningCenterContext.Tutorials.Where(t => t.IsActive)
            .Include(t => t.Sections).ToListAsync();

        return result;
    }

    public async Task<List<Tutorial>> GetSearchAsync(string name, int? year)
    {
        var result = await _learningCenterContext.Tutorials
            .Where(t => t.IsActive && t.Name.Contains(name) && t.Year >= year)
            .Include(t => t.Sections).ToListAsync(); //1000 Reigtros

        return result;
    }

    public async Task<Tutorial> GetById(int id)
    {
        return await _learningCenterContext.Tutorials.Where(t => t.Id == id && t.IsActive)
            .Include(t => t.Sections)
            .FirstOrDefaultAsync();
    }

    public async Task<Tutorial> GetByNameAsync(string name)
    {
        return await _learningCenterContext.Tutorials.Where(t => t.Name == name && t.IsActive).FirstOrDefaultAsync();
    }

    public async Task<int> SaveAsync(Tutorial data)
    {
        using (var transaction = await _learningCenterContext.Database.BeginTransactionAsync())
        {
            try
            {
                data.IsActive = true;
                _learningCenterContext.Tutorials.Add(data); //no se refleja en BBDD
                await _learningCenterContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }


        return data.Id;
    }

    public async Task<bool> Update(Tutorial data, int id)
    {
        var exitingTutorial = _learningCenterContext.Tutorials.Where(t => t.Id == id).FirstOrDefault();
        exitingTutorial.Name = data.Name;
        exitingTutorial.Description = data.Description;

        _learningCenterContext.Tutorials.Update(exitingTutorial);

        await _learningCenterContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool>  Delete(int id)
    {
        var exitingTutorial = _learningCenterContext.Tutorials.Where(t => t.Id == id).FirstOrDefault();

        // _learningCenterContext.Tutorials.Remove(exitingTutorial);
        exitingTutorial.IsActive = false;

        _learningCenterContext.Tutorials.Update(exitingTutorial);

        await _learningCenterContext.SaveChangesAsync();
        return true;
    }
}