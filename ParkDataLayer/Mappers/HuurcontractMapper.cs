using ParkBusinessLayer.Model;
using ParkDataLayer.Entitites;

namespace ParkDataLayer.Mappers
{
    public static class HuurcontractMapper
    {
        public static HuurcontractEF MapHuurcontract(Huurcontract contract)
        {
            return new HuurcontractEF
            {
                Id = contract.Id,
                StartDatum = contract.Huurperiode.StartDatum,
                EindDatum = contract.Huurperiode.EindDatum,
                HuisId = contract.Huis.Id,
                HuurderId = contract.Huurder.Id
            };
        }

        public static Huurcontract MapHuurcontractEF(HuurcontractEF contractEf)
        {
            var huurperiode = new Huurperiode(contractEf.StartDatum, (contractEf.EindDatum - contractEf.StartDatum).Days);
            var huis = HuisMapper.MapHuisEF(contractEf.HuisEf);
            var huurder = HuurderMapper.MapHuurderEF(contractEf.HuurderEf);

            return new Huurcontract(contractEf.Id, huurperiode, huurder, huis);
        }
    }
}