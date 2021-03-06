﻿namespace Linn.Production.Domain.LinnApps
{
    using System.ComponentModel.DataAnnotations;

    public static class LabelPrinters
    {
        public enum Printers
        {
            [Display(Name = "Goods In Labels (Large)")] GILabels,
            [Display(Name = "Goods In Labels (Small)")] GISmall,
            [Display(Name = "Production Label Printer (Small)")] ProdLbl1,
            [Display(Name = "Production Label Printer (Large)")] ProdLbl2,
            [Display(Name = "Metalwork Labels")] Metalwork,
            [Display(Name = "SMT Labels")] SMTSmall,
            [Display(Name = "Test Label")] TestLabels2,
            [Display(Name = "Mat Hand Labels")] MatHandLabels,
            [Display(Name = "LP12 Small (B133)")] LP12Small,
            [Display(Name = "LP12 Large (B133)")] LP12Large,
            [Display(Name = "DSM Small (B57)")] DSMSmall,
            [Display(Name = "DSM Large (BB7)")] DSMLarge,
            [Display(Name = "Flexible Small(B104)")] FlexibleSmall,
            [Display(Name = "Flexible Large (B104)")] FlexibleLarge,
            [Display(Name = "Kiko Small (op B30)B")] KikoSmall,
            [Display(Name = "Kiko Large (op B30)")] KikoLarge
        }
    }
}
