using IS_Proj_HIT.Models;
using System;
using System.Collections.Generic;
using IS_Proj_HIT.Models.Enum;
using IS_Proj_HIT.Models.PCA;
using IS_Proj_HIT.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace IS_Proj_HIT.ViewModels
{
    public static class AssessmentFormPageModelExtensions
    {
        public static PcaRecord ToPcaRecord(this AssessmentFormPageModel model) => new PcaRecord
        {
            PcaId = model.Pcaid,
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
            

            var weight = ConversionService.ConvertWeight(Enum.Parse<WeightUnit>(model.WeightUnit),
                                                         WeightUnit.Pounds, model.Weight);


            var headCirc = ConversionService.ConvertLength(Enum.Parse<LengthUnit>(model.HeadCircUnit),
                                                           LengthUnit.Inches, model.HeadCircumference);



            var bmi = model.BodyMassIndex;


            return assessments;
        }
    }
}