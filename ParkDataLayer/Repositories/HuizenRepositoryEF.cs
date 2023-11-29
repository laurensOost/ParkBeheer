using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using System;
using System.Linq;
using ParkDataLayer.Context;

namespace ParkDataLayer.Repositories
{
    public class HuizenRepositoryEF : IHuizenRepository
    {
        private readonly ParkContext _context;
        
        public HuizenRepositoryEF(ParkContext dbContext)
        {
            _context = dbContext;
        }
        public Huis GeefHuis(int id)
        {
            var huisEf = _context.Huizen.FirstOrDefault(h => h.Id == id); // linq queries 
            return huisEf != null ? HuisMapper.MapHuisEF(huisEf) : null;
        }

        public bool HeeftHuis(string straat, int nummer, Park park)
        {
            return _context.Huizen.Any(h => h.Straat == straat && h.Nr == nummer && h.ParkId == park.Id);
        }

        public bool HeeftHuis(int id)
        {
            return _context.Huizen.Any(h => h.Id == id);
        }

        public void UpdateHuis(Huis huis)
        {
            var huisEf = _context.Huizen.FirstOrDefault(h => h.Id == huis.Id);
            if (huisEf != null)
            {
                huisEf.Straat = huis.Straat;
                huisEf.Nr = huis.Nr;
                huisEf.Actief = huis.Actief;
                huisEf.ParkId = huis.Park.Id;
                _context.SaveChanges();
            }
        }

        public Huis VoegHuisToe(Huis h)
        {
            var huisEf = HuisMapper.MapHuis(h);
            _context.Huizen.Add(huisEf);
            _context.SaveChanges();
            return HuisMapper.MapHuisEF(huisEf);
        }
    }
}
