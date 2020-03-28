using IS_Proj_HIT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IS_Proj_HIT.Models.Enum;
using IS_Proj_HIT.Models.PCA;

namespace IS_Proj_HIT.ViewModels
{
    public class AssessmentFormPageModel
    {
        public int Pcaid { get; set; }
        [Required] public long EncounterId { get; set; }
        [Required] public long PatientMrn { get; set; }

        public decimal? Temperature { get; set; }
        public string TempUnit { get; set; }
        public int? TempRouteTypeId { get; set; }

        public decimal? Height { get; set; }
        public string HeightUnit { get; set; }

        public decimal? Weight { get; set; }
        public string WeightUnit { get; set; }

        public decimal? HeadCircumference { get; set; }
        public string HeadCircUnit { get; set; }

        public decimal? BodyMassIndex { get; set; }
        public string BodyMassIndexUnit { get; set; }

        public byte? Pulse { get; set; }
        public byte? PulseOximetry { get; set; }
        public int? PulseRouteTypeId { get; set; }
        public string PulseUnit { get; set; }

        public byte? Respiration { get; set; }
        public string RespUnit { get; set; }
        public string OxygenFlow { get; set; }
        public int? O2deliveryTypeId { get; set; }

        public int? PainScaleTypeId { get; set; }

        public short? SystolicBloodPressure { get; set; }
        public short? DiastolicBloodPressure { get; set; }
        public string BpRouteUnit { get; set; }
        public string BpLocationUnit { get; set; }

        public AssessmentFormPageModel()
        {
        }

        public AssessmentFormPageModel(PcaRecord pca)
        {
            Pcaid = pca.PcaId;
            EncounterId = pca.EncounterId;
            Temperature = pca.Temperature;
            TempRouteTypeId = pca.TempRouteTypeId;
            Pulse = pca.Pulse;
            PulseOximetry = pca.PulseOximetry;
            PulseRouteTypeId = pca.PulseRouteTypeId;
            Respiration = pca.Respiration;
            OxygenFlow = pca.OxygenFlow;
            O2deliveryTypeId = pca.O2deliveryTypeId;
            PainScaleTypeId = pca.PainScaleTypeId;
            SystolicBloodPressure = pca.SystolicBloodPressure;
            DiastolicBloodPressure = pca.DiastolicBloodPressure;
            //if (decimal.TryParse(
            //    pca.CareSystemAssessment.FirstOrDefault(a =>
            //            (SystemAssessmentTypeEnum) a.CareSystemParameterId is SystemAssessmentTypeEnum.Height)
            //        ?.Comment, out var height))
            //    Height = height;
            //if (decimal.TryParse(
            //    pca.CareSystemAssessment.FirstOrDefault(a =>
            //            (SystemAssessmentTypeEnum) a.CareSystemParameterId is SystemAssessmentTypeEnum.Weight)
            //        ?.Comment, out var weight))
            //    Weight = weight;
            //if (decimal.TryParse(
            //    pca.CareSystemAssessment.FirstOrDefault(a =>
            //        (SystemAssessmentTypeEnum) a.CareSystemParameterId is SystemAssessmentTypeEnum
            //            .HeadCircumference)?.Comment, out var headCircumference))
            //    HeadCircumference = headCircumference;
            //if (decimal.TryParse(
            //    pca.CareSystemAssessment.FirstOrDefault(a =>
            //            (SystemAssessmentTypeEnum) a.CareSystemParameterId is SystemAssessmentTypeEnum
            //                .BodyMassIndex)
            //        ?.Comment, out var bodyMassIndex))
            //    BodyMassIndex = bodyMassIndex;
        }
    }
}