using ParkBusinessLayer.Model;
using ParkDataLayer.Entitites;
using ParkDataLayer.Mappers;

public static class HuisMapper
{
    
public static HuisEF MapHuis(Huis huis)
    {
        return new HuisEF()
        {
            Id = huis.Id,
            Straat = huis.Straat,
            Nr = huis.Nr,
            Actief = huis.Actief,
            ParkId = huis.Park.Id
        };
    }

    public static Huis MapHuisEF(HuisEF huisEf)
    {
        return new Huis(huisEf.Id, huisEf.Straat, huisEf.Nr, huisEf.Actief, ParkMapper.MapParkEF(huisEf.ParkEf));
    }
    
}