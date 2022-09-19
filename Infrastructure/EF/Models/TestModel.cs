using System.ComponentModel.DataAnnotations;

namespace Infrastructure.EF.Models;

public class TestModel : Entity
{
    public string name { get; set; }
    public int value { get; set; }
}