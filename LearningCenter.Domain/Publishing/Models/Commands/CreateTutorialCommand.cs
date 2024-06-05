using System.ComponentModel.DataAnnotations;

namespace Presentation.Request;

public class CreateTutorialCommand
{
    [Required] public string Name { get; set; }

    public string Description { get; set; }

    [Required] [Range(1990, 2024)] public int Year { get; set; }

    [Required] [Range(1, 100)] public int Quantity { get; set; }

    public List<SectionRequest> Sections { get; set; }
}