using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsPrestationService.Models;

[Table("Etat_Prestation")]
public class EtatPrestation
{
    [Key]
    [Column("id_etat_prestation")]
    public int IdEtatPrestation { get; set; }
    [Column("niveau")]
    public int Niveau { get; set; }
    [Column("designation")]
    public string Designation { get; set; }
}