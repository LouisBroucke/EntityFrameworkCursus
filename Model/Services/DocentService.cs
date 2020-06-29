using Model.Entities;
using Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Services
{
    public class DocentService
    {
        readonly private EFOpleidingenContext _context;

        public DocentService(EFOpleidingenContext context)
        {
            _context = context;
        }

        public IEnumerable<Docent> GetDocentenVoorCampus(int campusId)
        {
            //throw new NotImplementedException();
            return _context.Docenten.Where(x => x.CampusId == campusId).ToList();
        }

        public Docent GetDocent(int id)
        {
            throw new NotImplementedException();
        }

        public void ToevoegenDocent(Docent docent)
        {
            throw new NotImplementedException();
        }
    }
}
