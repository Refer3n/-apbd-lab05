using System.ComponentModel.DataAnnotations;

namespace Lab05.DTOs;

public class CreateReservationDto : IValidatableObject
{
    [Range(1, int.MaxValue, ErrorMessage = "RoomId must be greater than zero.")]
    public int RoomId { get; set; }

    [Required] public string OrganizerName { get; set; } = string.Empty;

    [Required] public string Topic { get; set; } = string.Empty;

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    [Required] public string Status { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult("EndTime must be greater than StartTime.");
        }
    }
}
