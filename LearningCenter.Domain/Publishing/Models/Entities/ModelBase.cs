using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class ModelBase
{
    public int Id { get; set; }

    public int CreatedUser { get; set; }

    public int? UpdatedUser { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
    public bool IsActive { get; set; } = true;
}