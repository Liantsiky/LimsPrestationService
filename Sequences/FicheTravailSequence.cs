using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsPrestationService.Sequences;

[Table("fiche_travail_sequence")]
public class FicheTravailSequence
{
    [Key]
    [Column("annee")]
    public int Annee {get; set; }
    [Column("dernier_valeur")]
    public int DerniereValeur {get; set;}
}