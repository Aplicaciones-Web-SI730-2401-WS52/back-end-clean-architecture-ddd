using System.ComponentModel.DataAnnotations;


namespace Presentation.Request;

public class SectionRequest
{
    [Required] public string Title { get; set; }
}