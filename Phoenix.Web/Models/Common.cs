using System.ComponentModel.DataAnnotations;

namespace Phoenix.Web.Models
{
    public enum PARTY_DISPOSAL_CAUSE : byte
    {
        [Display(Name = "За власним бажанням")]
        AtWill = 0,

        [Display(Name = "За рішенням партії")]
        PartyDecision = 1,
    }
}
