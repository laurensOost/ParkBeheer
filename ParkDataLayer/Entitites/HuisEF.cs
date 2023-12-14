using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkDataLayer.Entitites
{
    public class HuisEF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(250)]
        public string Straat { get; set; }

        [Required]
        public int Nr { get; set; }

        [Required]
        public bool Actief { get; set; }

        // ForeignKey attribute should be on the navigation property
        [Required]
        public string ParkId { get; set; }

        [ForeignKey("ParkId")]
        public virtual ParkEF ParkEf { get; set; }

        public ICollection<HuurcontractEF> Huurcontracten { get; set; } = new List<HuurcontractEF>(); // nota's Tom, entity heeft geen dictionaries, dus gaat iets anders moeten doen, = navigation property

        // Default constructor
        public HuisEF() { }
    }
}