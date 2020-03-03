using IS_Proj_HIT.Models;
using System;
using System.Collections.Generic;

namespace IS_Proj_HIT.ViewModels
{
    public static class AssessmentFormPageModelExtensions
    {
        public static Pcarecord ToPcaRecord(this AssessmentFormPageModel model) => new Pcarecord
        {
            Pcaid = model.Pcaid,
            EncounterId = model.EncounterId,
            TempRouteTypeId = model.TempRouteTypeId,
            PulseRouteTypeId = model.PulseRouteTypeId,
            O2deliveryTypeId = model.O2deliveryTypeId,
            PainScaleTypeId = model.PainScaleTypeId,
            Temperature = model.Temperature,
            Pulse = model.Pulse,
            Respiration = model.Respiration,
            SystolicBloodPressure = model.SystolicBloodPressure,
            DiastolicBloodPressure = model.DiastolicBloodPressure,
            PulseOximetry = model.PulseOximetry,
            OxygenFlow = model.OxygenFlow,
            LastModified = DateTime.Now,
            DateVitalsAdded = DateTime.Now
        };

        public static IList<CareSystemAssessment> ToSystemAssessments(this AssessmentFormPageModel model, int? pcaId = null) =>
            new List<CareSystemAssessment>
            {
                new CareSystemAssessment((int)SystemAssessmentTypeEnum.Height)
                {
                    Pcaid = pcaId ?? model.Pcaid,
                    //WdlEx = model.Height != null && model.Height != 0,
                    CareSystemComment = model.Height.ToString(),
                    DateCareSystemAdded = DateTime.Now,
                    LastModified = DateTime.Now
                },
                new CareSystemAssessment((int)SystemAssessmentTypeEnum.Weight)
                {
                    Pcaid = pcaId ?? model.Pcaid,
                    //WdlEx = model.Weight != null && model.Weight != 0,
                    CareSystemComment = model.Weight.ToString(),
                    DateCareSystemAdded = DateTime.Now,
                    LastModified = DateTime.Now
                },
                new CareSystemAssessment((int)SystemAssessmentTypeEnum.HeadCircumference)
                {
                    Pcaid = pcaId ?? model.Pcaid,
                    //WdlEx = model.HeadCircumference != null && model.HeadCircumference != 0,
                    CareSystemComment = model.HeadCircumference.ToString(),
                    DateCareSystemAdded = DateTime.Now,
                    LastModified = DateTime.Now
                },
                new CareSystemAssessment((int)SystemAssessmentTypeEnum.BodyMassIndex)
                {
                    Pcaid = pcaId ?? model.Pcaid,
                    //WdlEx = model.BodyMassIndex != null && model.BodyMassIndex != 0,
                    CareSystemComment = model.BodyMassIndex.ToString(),
                    DateCareSystemAdded = DateTime.Now,
                    LastModified = DateTime.Now
                },
            };
    }
}