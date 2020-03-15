using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.PCA;

namespace IS_Proj_HIT.ViewModels
{
    public class CareAssessmentPageModel
    {
        public PcaRecord Assessment { get; set; }
        public Encounter Encounter { get; set; }
        public Patient Patient { get; set; }
    }
}