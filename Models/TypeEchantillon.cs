using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsPrestationService.Models;
[Table("Type_echantillon")]
public class TypeEchantillon 
{
    [Key]
    [Column("id_type_echantillon")]
    public int IdTypeEchantillon { get; set; }
    [Column("designation")]
    public string Designation { get; set; }
}