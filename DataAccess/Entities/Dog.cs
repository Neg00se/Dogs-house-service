using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;
public class Dog
{
    [Key]
    [Column("name")]
    public string Name { get; set; }

    [Column("color")]
    [MaxLength(255)]
    public string Color { get; set; }

    [Column("tail_length")]
    public int TailLength { get; set; }

    [Column("weight")]
    public int Weight { get; set; }
}
