using System;
using System.Collections.Generic;

namespace IS_Proj_HIT.Models.PCA
{
    public partial class PcaRecord
    {
        public PcaRecord()
        {
            CareSystemAssessment = new HashSet<CareSystemAssessment>();
            PcaComment = new HashSet<PcaComment>();
        }

        public int PcaId { get; set; }
        public long EncounterId { get; set; }

        public int? TempRouteTypeId { get; set; }
        public int? PulseRouteTypeId { get; set; }
        public int? O2deliveryTypeId { get; set; }
        public int? PainScaleTypeId { get; set; }
        public byte? PainLevelGoal { get; set; }
        public byte? BloodPressureRouteTypeId { get; set; }
        public byte? BmiMethodId { get; set; }

        public decimal? Temperature { get; set; }
        public byte? Pulse { get; set; }
        public byte? PulseOximetry { get; set; }
        public byte? Respiration { get; set; }
        public decimal? PercentOxygenDelivered { get; set; }
        public string OxygenFlow { get; set; }
        public short? SystolicBloodPressure { get; set; }
        public short? DiastolicBloodPressure { get; set; }
        public decimal? Weight { get; set; }
        public string WeightUnits { get; set; }
        public decimal? Height { get; set; }
        public string HeightUnits { get; set; }
        public decimal? HeadCircumference { get; set; }
        public string HeadCircumferenceUnits { get; set; }
        public decimal? BodyMassIndex { get; set; }
        public DateTime? DateVitalsAdded { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Encounter Encounter { get; set; }
        public virtual BmiMethod BmiMethod { get; set; }
        public virtual BloodPressureRouteType BloodPressureRouteType { get; set; }
        public virtual O2deliveryType O2deliveryType { get; set; }
        public virtual PainScaleType PainScaleType { get; set; }
        public virtual PulseRouteType PulseRouteType { get; set; }
        public virtual TempRouteType TempRouteType { get; set; }
        public virtual ICollection<CareSystemAssessment> CareSystemAssessment { get; set; }
        public virtual ICollection<PcaPainAssessment> PainAssessment { get; set; }
        public virtual ICollection<PcaComment> PcaComment { get; set; }
    }
}