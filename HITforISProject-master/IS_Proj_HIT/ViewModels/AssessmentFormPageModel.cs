using IS_Proj_HIT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IS_Proj_HIT.Models.Enum;

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

        public byte? Pulse { get; set; }
        public byte? PulseOximetry { get; set; }
        public int? PulseRouteTypeId { get; set; }
        public byte? Respiration { get; set; }
        public string OxygenFlow { get; set; }
        public int? O2deliveryTypeId { get; set; }
        public int? PainScaleTypeId { get; set; }
        public byte? SystolicBloodPressure { get; set; }
        public byte? DiastolicBloodPressure { get; set; }

        public AssessmentFormPageModel()
        {
        }

        public AssessmentFormPageModel(Pcarecord pca)
        {
            Pcaid = pca.Pcaid;
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
            if (decimal.TryParse(
                pca.CareSystemAssessment.FirstOrDefault(a =>
                        (SystemAssessmentTypeEnum) a.CareSystemAssessmentTypeId is SystemAssessmentTypeEnum.Height)
                    ?.CareSystemComment, out var height))
                Height = height;
            if (decimal.TryParse(
                pca.CareSystemAssessment.FirstOrDefault(a =>
                        (SystemAssessmentTypeEnum) a.CareSystemAssessmentTypeId is SystemAssessmentTypeEnum.Weight)
                    ?.CareSystemComment, out var weight))
                Weight = weight;
            if (decimal.TryParse(
                pca.CareSystemAssessment.FirstOrDefault(a =>
                    (SystemAssessmentTypeEnum) a.CareSystemAssessmentTypeId is SystemAssessmentTypeEnum
                        .HeadCircumference)?.CareSystemComment, out var headCircumference))
                HeadCircumference = headCircumference;
            if (decimal.TryParse(
                pca.CareSystemAssessment.FirstOrDefault(a =>
                        (SystemAssessmentTypeEnum) a.CareSystemAssessmentTypeId is SystemAssessmentTypeEnum
                            .BodyMassIndex)
                    ?.CareSystemComment, out var bodyMassIndex))
                BodyMassIndex = bodyMassIndex;
        }
    }
}