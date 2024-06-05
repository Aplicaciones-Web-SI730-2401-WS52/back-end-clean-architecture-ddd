
using Domain;
using LearningCenter.Domain.Security.Models;

namespace LearningCenter.Domain.Publishing.Models.Aggregates;

public class Reservation : ModelBase
{
    public User User { get; set; }
    public Tutorial Tutorial { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

