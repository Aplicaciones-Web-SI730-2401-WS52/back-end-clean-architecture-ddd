namespace _1_API.Response;

public class TutorialResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public int Quantity { get; set; }
    public List<SectionResponse> Sections { get; set; }
}