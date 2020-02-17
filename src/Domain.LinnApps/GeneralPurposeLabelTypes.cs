namespace Linn.Production.Domain.LinnApps
{
    using System.ComponentModel.DataAnnotations;

    public static class GeneralPurposeLabelTypes
    {
        public enum Labels
        {
            [Display(Name = "Large label (wee text)")] LargeWeeText = 0,
            [Display(Name = "Large label (big text)")] LargeBigText = 1,
            [Display(Name = "Small")] Small = 2,
            [Display(Name = "Address Label")] AddressLabel = 4,
            [Display(Name = "Goods In Label")] GoodsInLabel = 5,
            [Display(Name = "Small (wee text)")] SmallWeeText = 6,
            [Display(Name = "Small (wee bold text)")] SmallBoldText = 7
        }
    }
}

// If these change again ^ then remember to update the front end too
