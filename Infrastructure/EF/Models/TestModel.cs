using System.ComponentModel.DataAnnotations;

namespace Infrastructure.EF.Models;

public class TestModel
{
    [Key] public int Id { get; set; }

    public string name { get; set; }
    public int value { get; set; }
}