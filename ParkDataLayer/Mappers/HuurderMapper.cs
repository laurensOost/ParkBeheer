using ParkBusinessLayer.Model;
using ParkDataLayer.Entitites;

namespace ParkDataLayer.Mappers;

public class HuurderMapper
{
    
    public static HuurderEF MapHuurder(Huurder huurder)
    {
        return new HuurderEF()
        {
            Id = huurder.Id,
            Naam = huurder.Naam,
            Telefoon = huurder.Contactgegevens.Tel,
            Email = huurder.Contactgegevens.Email,
            Adres = huurder.Contactgegevens.Adres
        };
    }
    
    public static Huurder MapHuurderEF(HuurderEF huurderEf)
    {
        var contactgegevens = new Contactgegevens(huurderEf.Telefoon, huurderEf.Email, huurderEf.Adres);
        return new Huurder(huurderEf.Id, huurderEf.Naam, contactgegevens);
    }
    
}