using Domain;

namespace Domain;

public interface ITutorialRepository
{
    Task<List<Tutorial>> GetAllAsync();
    Task<List<Tutorial>> GetSearchAsync(string name, int? year);

    Task<Tutorial> GetById(int id);

    Task<Tutorial> GetByNameAsync(string name);

    Task<int> SaveAsync(Tutorial data);

    Task<bool> Update(Tutorial data, int id);

    Task<bool> Delete(int id);
}