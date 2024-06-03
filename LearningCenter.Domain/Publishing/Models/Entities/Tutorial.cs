namespace Domain;

public class Tutorial : ModelBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }

    public int Quantity { get; set; }
    public List<Section> Sections { get; set; }
}