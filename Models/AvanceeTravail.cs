using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsPrestationService.Models;

[Table("Avancee_travail")]
public class AvanceeTravail
{
    [Key]
    [Column("id_avancee_travail")]
    public int IdAvanceeTravail { get; set; }
    [Column("designation")]
    public required string Designation { get; set; }
    [Column("niveau")]
    public int Niveau { get; set; }
}