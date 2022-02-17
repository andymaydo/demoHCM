using static HCMModels.CaseModel;

namespace HCM.Models
{
    public class CaseOriginatorSelectable : CaseOriginator
    {
        public bool Selected { get; set; }
    }
}
