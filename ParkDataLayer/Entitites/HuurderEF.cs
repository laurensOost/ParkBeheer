using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkDataLayer.Entitites;

public class HuurderEF
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Naam { get; set; }

    // If Contactgegevens is a complex type you will need to create separate columns for each piece of contact information
    // For example:
    [StringLength(100)]
    public string Telefoon { get; set; }
        
    [StringLength(100)]
    public string Email { get; set; }
        
    [StringLength(100)]
    public string Adres { get; set; }

    public ICollection<HuurcontractEF> Huurcontracten { get; set; } = new List<HuurcontractEF>();
}
