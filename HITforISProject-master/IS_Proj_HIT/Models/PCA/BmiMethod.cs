using System;
using System.Collections;
using System.Collections.Generic;

namespace IS_Proj_HIT.Models.PCA
{
    public class BmiMethod
    {
        public BmiMethod()
        {
            PcaRecords = new HashSet<PcaRecord>();
        }

        public byte BmiMethodId { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }

        public ICollection<PcaRecord> PcaRecords { get; set; }
    }
}