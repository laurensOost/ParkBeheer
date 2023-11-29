using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ParkBusinessLayer.Model;

namespace ParkDataLayer.Entitites;

public class ParkEF
{
    [Key]
    [StringLength(20)]
    public string Id { get; set; }

    [Required]
    [StringLength(250)]
    public string Naam { get; set; }

    [StringLength(500)]
    public string Locatie { get; set; }

    public ICollection<HuisEF> Huizen { get; set; } = new List<HuisEF>();
}