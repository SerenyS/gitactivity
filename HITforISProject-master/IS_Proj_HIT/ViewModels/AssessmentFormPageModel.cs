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
        public PcaRecord PcaRecord { get; set; }

        [Required] public string PatientMrn { get; set; }

        public string TempUnit { get; set; }
        public string[] VitalNotes { get; set; }

        public List<CareSystemType> SecondarySystemTypes { get; set; }
        public Dictionary<int, CareSystemAssessment> Assessments { get; set; }

        public List<PainScaleType> PainScales { get; set; }
        public Dictionary<int, int?> PainRatings { get; set; }

        public AssessmentFormPageModel()
        {
            PcaRecord = new PcaRecord();
            VitalNotes = new string[9];

            SecondarySystemTypes = new List<CareSystemType>();
            Assessments = new Dictionary<int, CareSystemAssessment>();

            PainScales = new List<PainScaleType>();
            PainRatings = new Dictionary<int, int?>();
        }
    }
}