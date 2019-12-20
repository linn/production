namespace Linn.Production.Facade.Services

{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Products;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Facade.Extensions;

    public class LabelPrintService : ILabelPrintService
    {
        private readonly IBartenderLabelPack bartenderLabelPack;

        private readonly ILabelPack labelPack;

        private readonly IRepository<ProductData, int> productDataRepository;

        private readonly IRepository<SerialNumber, int> serialNumberRepository;

        private readonly ISernosPack sernosPack;

        private readonly IRepository<LabelType, string> labelTypeRepository;

        private string message;

        public LabelPrintService(
            IBartenderLabelPack bartenderLabelPack,
            ILabelPack labelPack,
            IRepository<ProductData, int> productDataRepository,
            IRepository<LabelType, string> labelTypeRepository)
        {
            this.labelPack = labelPack;
            this.bartenderLabelPack = bartenderLabelPack;
            this.productDataRepository = productDataRepository;
            this.labelTypeRepository = labelTypeRepository;
        }

        public IResult<IEnumerable<IdAndName>> GetPrinters()
        {
            var printerList = new List<IdAndName>();

            var allPrinterValues = Enum.GetValues(typeof(LabelPrinters.Printers));

            for (int i = 0; i < allPrinterValues.Length; i++)
            {
                var enumValue = (LabelPrinters.Printers)i;
                printerList.Add(new IdAndName(i, enumValue.GetDisplayName()));
            }

            return new SuccessResult<IEnumerable<IdAndName>>(printerList);
        }

        public IResult<IEnumerable<IdAndName>> GetLabelTypes()
        {
            var labelList = new List<IdAndName>();

            var allLabelValues = Enum.GetValues(typeof(GeneralPurposeLabelTypes.Labels));

            for (int i = 0; i < allLabelValues.Length; i++)
            {
                var enumValue = (GeneralPurposeLabelTypes.Labels)i;
                labelList.Add(new IdAndName(i, enumValue.GetDisplayName()));
            }

            return new SuccessResult<IEnumerable<IdAndName>>(labelList);
        }

        public void PrintLabel()
        {
            throw new NotImplementedException();
        }
    }
}