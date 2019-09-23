using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace isprojectHiT.Models
{
    public class EFWCTCHealthSystemRepository : IWCTCHealthSystemRepository
    {
        private WCTCHealthSystemContext context;
        public EFWCTCHealthSystemRepository(WCTCHealthSystemContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Ethnicity> Ethnicities => context.Ethnicity;
        public IQueryable<Gender> Genders => context.Gender;

        public IQueryable<Discharge> Discharges => context.Discharge;
    }
}
