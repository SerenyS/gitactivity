using IS_Proj_HIT.Models;
using System;
using System.Collections.Generic;
using IS_Proj_HIT.Models.Enum;
using IS_Proj_HIT.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
            Temperature = ConversionService.ConvertTemp(Enum.Parse<TempUnit>(model.TempUnit),
                                                        TempUnit.Fahrenheit, model.Temperature),
            Pulse = model.Pulse,
            Respiration = model.Respiration,
            SystolicBloodPressure = model.SystolicBloodPressure,
            DiastolicBloodPressure = model.DiastolicBloodPressure,
            PulseOximetry = model.PulseOximetry,
            OxygenFlow = model.OxygenFlow,
            LastModified = DateTime.Now,
            DateVitalsAdded = DateTime.Now
        };

        public static List<CareSystemAssessment> ToSystemAssessments(this AssessmentFormPageModel model, int? pcaId = null)
        {
            var assessments = new List<CareSystemAssessment>();
            var id = pcaId ?? model.Pcaid;

            var height = ConversionService.ConvertLength(Enum.Parse<LengthUnit>(model.HeightUnit),
                                                         LengthUnit.Inches, model.Height);
            if (height != null)
                assessments.Add(new CareSystemAssessment(SystemAssessmentTypeEnum.Height)
                {
                    Pcaid = id,
                    //WdlEx = model.Height != null && model.Height != 0,
                    CareSystemComment = height.ToString(),
                    LastModified = DateTime.Now
                });

            var weight = ConversionService.ConvertWeight(Enum.Parse<WeightUnit>(model.WeightUnit),
                                                         WeightUnit.Pounds, model.Weight);
            if (weight != null)
                assessments.Add(new CareSystemAssessment(SystemAssessmentTypeEnum.Weight)
                {
                    Pcaid = id,
                    //WdlEx = model.Weight != null && model.Weight != 0,
                    CareSystemComment = weight.ToString(),
                    LastModified = DateTime.Now
                });

            var headCirc = ConversionService.ConvertLength(Enum.Parse<LengthUnit>(model.HeadCircUnit),
                                                           LengthUnit.Inches, model.HeadCircumference);
            if (headCirc != null)
                assessments.Add(new CareSystemAssessment(SystemAssessmentTypeEnum.HeadCircumference)
                {
                    Pcaid = id,
                    //WdlEx = model.HeadCircumference != null && model.HeadCircumference != 0,
                    CareSystemComment = headCirc.ToString(),
                    LastModified = DateTime.Now
                });


            var bmi = model.BodyMassIndex;
            
            if (bmi != null)
                assessments.Add(new CareSystemAssessment(SystemAssessmentTypeEnum.BodyMassIndex)
                {
                    Pcaid = id,
                    //WdlEx = model.BodyMassIndex != null && model.BodyMassIndex != 0,
                    CareSystemComment = bmi.ToString(),
                    LastModified = DateTime.Now
                });

            var sbp = model.SystolicBloodPressure;

            if (sbp != null)
                assessments.Add(new CareSystemAssessment(SystemAssessmentTypeEnum.SystolicBloodPressure)
                {
                    Pcaid = id,
                    CareSystemComment = sbp.ToString(),
                    LastModified = DateTime.Now
                });

            var dbp = model.DiastolicBloodPressure;

            if (dbp != null)
                assessments.Add(new CareSystemAssessment(SystemAssessmentTypeEnum.DiastolicBloodPressure)
                {
                    Pcaid = id,
                    CareSystemComment = dbp.ToString(),
                    LastModified = DateTime.Now
                });


            return assessments;
        }
    }
}