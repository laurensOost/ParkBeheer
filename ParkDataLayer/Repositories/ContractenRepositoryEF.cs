using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Context;
using ParkDataLayer.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class ContractenRepositoryEF : IContractenRepository
    {
        private readonly ParkContext _context;

        public ContractenRepositoryEF(ParkContext dbContext)
        {
            _context = dbContext;
        }

        public void AnnuleerContract(Huurcontract contract)
        {
            var contractEf = _context.Huurcontracten.Find(contract.Id);
            if (contractEf != null)
            {
                _context.Huurcontracten.Remove(contractEf);
                _context.SaveChanges();
            }
        }

        public Huurcontract GeefContract(string id)
        {
            var contractEf = _context.Huurcontracten
                .Include(c => c.HuisEf)
                .Include(c => c.HuurderEf)
                .FirstOrDefault(c => c.Id == id);

            return contractEf != null ? HuurcontractMapper.MapHuurcontractEF(contractEf) : null;
        }

        public List<Huurcontract> GeefContracten(DateTime dtBegin, DateTime? dtEinde) // range van data meegeven, met optioneel einddatum
        {
            var query = _context.Huurcontracten
                .Include(c => c.HuisEf)
                .Include(c => c.HuurderEf)
                .Where(c => c.StartDatum >= dtBegin); // filtert op startdatum >= dtBegin 

            if (dtEinde.HasValue) // als dtEinde een waarde heeft, filtert op einddatum <= dtEinde, requirements 
            {
                query = query.Where(c => c.StartDatum <= dtEinde.Value);
            }

            return query.Select(HuurcontractMapper.MapHuurcontractEF).ToList();
        }

        public bool HeeftContract(DateTime startDatum, int huurderid, int huisid)
        {
            return _context.Huurcontracten.Any(c => c.StartDatum == startDatum && c.HuurderId == huurderid && c.HuisId == huisid);
        }

        public bool HeeftContract(string id)
        {
            return _context.Huurcontracten.Any(c => c.Id == id);
        }

        public void UpdateContract(Huurcontract contract)
        {
            var contractEf = _context.Huurcontracten.Find(contract.Id);
            if (contractEf != null)
            {
                _context.Entry(contractEf).CurrentValues.SetValues(HuurcontractMapper.MapHuurcontract(contract));
                _context.SaveChanges();
            }
        }

        public void VoegContractToe(Huurcontract contract)
        {
            var contractEf = HuurcontractMapper.MapHuurcontract(contract);
            _context.Huurcontracten.Add(contractEf);
            _context.SaveChanges();
        }
    }
}
