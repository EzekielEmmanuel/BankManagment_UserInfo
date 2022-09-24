using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Models;

[Index("Number", IsUnique = true)]
public class BankAccount : Entity
{
    [ForeignKey("ApplicationUser")]
    [Required]
    public string UserId { get; set; }

    [Required] public string Number { get; set; }

    [Required] public string Type { get; set; }
}