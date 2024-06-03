namespace _3_Data;

public class TutorialOracle :ITutorialData
{
    public List<Tutorial> getAll()
    {
        // COnecta BBDD Oracle
        List<Tutorial> data = new List<Tutorial>();
        data.Add(new Tutorial(){ Name = "tutorial 1 Oracle"});
        data.Add(new Tutorial(){ Name = "tutorial 2 Oracle"});
        data.Add(new Tutorial(){ Name = "tutorial 3 Oracle"});

        return data;
    }

    public Tutorial GetById(int id)
    {
        throw new NotImplementedException();
    }

    public bool Save(Tutorial data)
    {
        throw new NotImplementedException();
    }

    public bool Update(Tutorial data, int id)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }
}