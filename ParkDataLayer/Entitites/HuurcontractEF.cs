using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParkBusinessLayer.Model;

namespace ParkDataLayer.Entitites;

public class HuurcontractEF
{
    [Key]
    [StringLength(25)]
    public string Id { get; set; }

    [Required]
    public DateTime StartDatum { get; set; }

    [Required]
    public DateTime EindDatum { get; set; }
    
    [NotMapped]
    public int AantalDagen => (EindDatum - StartDatum).Days;

    [Required]
    [ForeignKey("HuisEf")]
    public int HuisId { get; set; }
    public virtual HuisEF HuisEf { get; set; }

    [Required]
    [ForeignKey("HuurderEf")]
    public int HuurderId { get; set; }
    public HuurderEF HuurderEf { get; set; }
}