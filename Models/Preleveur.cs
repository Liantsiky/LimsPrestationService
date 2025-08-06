using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsPrestationService.Models;

[Table("Preleveur")]
public class Preleveur
{
    [Key]
    [Column("id_preleveur")]
    public int IdPreleveur { get; set; }

    [Column("designation")]
    public required string Designation { get; set; }
}