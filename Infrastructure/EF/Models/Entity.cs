using System.ComponentModel.DataAnnotations;

namespace Infrastructure.EF.Models;

public abstract class Entity
{
    [Key] public int Id { get; set; }

    public DateTimeOffset MetaAddedDate { get; set; }
    public string MetaAddedUser { get; set; }
    public string? MetaModifiedUser { get; set; }
    public DateTimeOffset? MetaModifiedDate { get; set; }
}