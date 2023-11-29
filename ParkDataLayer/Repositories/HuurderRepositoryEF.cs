using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using ParkDataLayer.Context;
using ParkDataLayer.Mappers;

namespace ParkDataLayer.Repositories
{
    public class HuurderRepositoryEF : IHuurderRepository
    {
        private readonly ParkContext _context;

        public HuurderRepositoryEF(ParkContext context)
        {
            _context = context;
        }
        public Huurder GeefHuurder(int id)
        {
            var huurderEf = _context.Huurders.FirstOrDefault(h => h.Id == id);
            return huurderEf != null ? HuurderMapper.MapHuurderEF(huurderEf) : null;
        }

        public List<Huurder> GeefHuurders(string naam)
        {
            var huurdersEf = _context.Huurders.Where(h => h.Naam.Contains(naam)).ToList();
            return huurdersEf.Select(HuurderMapper.MapHuurderEF).ToList();
        }

        public bool HeeftHuurder(string naam, Contactgegevens contact)
        {
            return _context.Huurders.Any(h => h.Naam == naam && 
                                              h.Telefoon == contact.Tel && 
                                              h.Email == contact.Email && 
                                              h.Adres == contact.Adres);
        }

        public bool HeeftHuurder(int id)
        {
            return _context.Huurders.Any(h => h.Id == id);
        }

        public void UpdateHuurder(Huurder huurder)
        {
            var huurderEf = _context.Huurders.FirstOrDefault(h => h.Id == huurder.Id);
            if (huurderEf != null)
            {
                huurderEf.Naam = huurder.Naam;
                huurderEf.Telefoon = huurder.Contactgegevens.Tel;
                huurderEf.Email = huurder.Contactgegevens.Email;
                huurderEf.Adres = huurder.Contactgegevens.Adres;
                _context.SaveChanges();
            }
        }

        public Huurder VoegHuurderToe(Huurder h)
        {
            var huurderEf = HuurderMapper.MapHuurder(h);
            _context.Huurders.Add(huurderEf);
            _context.SaveChanges();
            return HuurderMapper.MapHuurderEF(huurderEf);
        }
    }
}
