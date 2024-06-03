using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Section : ModelBase
{
    [Required] [MaxLength(90)] public string Title { get; set; }

    [Required] [Range(1, int.MaxValue)] public int Chapter { get; set; }

    public int TutorialId { get; set; }
    public Tutorial Tutorial { get; set; }
}