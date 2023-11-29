using ParkBusinessLayer.Model;
using ParkDataLayer.Entitites;

namespace ParkDataLayer.Mappers;

public class ParkMapper
{
    public static ParkEF MapPark(Park park)
    {
        return new ParkEF()
        {
            Id = park.Id,
            Naam = park.Naam,
            Locatie = park.Locatie
        };
    }

    public static Park MapParkEF(ParkEF parkEf)
    {
        return new Park(parkEf.Id, parkEf.Naam, parkEf.Locatie);
    }
    
}