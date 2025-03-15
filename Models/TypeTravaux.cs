
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsPrestationService.Models;

[Table("Type_travaux")]
public class TypeTravaux
{
    [Key]
    [Column("id_type_travaux")]
    public int IdTypeTravaux { get; set; }
    [Column("code")]
    public string Code { get; set; }
    [Column("designation")]
    public string Designation { get; set; }
    [Column("has_resultat")]
    public int HasResultat { get; set; }
    [Column("id_departement")]
    public int IdDepartement { get; set; }
    [Column("date_creation")]
    public DateTime? DateCreation { get; set; }
    [Column("have_formule")]
    public int HaveFormule { get; set; }
}