namespace Linn.Production.Domain.LinnApps
{
    using System.ComponentModel.DataAnnotations;

    public static class GeneralPurposeLabelTypes
    {
        public enum Labels
        {
            [Display(Name = "Large label (wee text)")] LargeWeeText,
            [Display(Name = "Large label (big text)")] LargeBigText,
            [Display(Name = "Small")] Small,
            [Display(Name = "Address Label")] AddressLabel,
            [Display(Name = "Goods In Label")] GoodsInLabel,
            [Display(Name = "Small (wee text)")] SmallWeeText,
            [Display(Name = "Small (wee bold text)")] SmallBoldText
        }
    }
}
