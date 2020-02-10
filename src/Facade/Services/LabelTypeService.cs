namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class LabelTypeService : FacadeService<LabelType, string, LabelTypeResource, LabelTypeResource>
    {
        public LabelTypeService(IRepository<LabelType, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override LabelType CreateFromResource(LabelTypeResource resource)
        {
            return new LabelType
                       {
                           LabelTypeCode = resource.LabelTypeCode,
                           BarcodePrefix = resource.BarcodePrefix,
                           CommandFilename = resource.CommandFilename,
                           DefaultPrinter = resource.DefaultPrinter,
                           Description = resource.Description,
                           Filename = resource.Filename,
                           NSBarcodePrefix = resource.NSBarcodePrefix,
                           TestCommandFilename = resource.TestCommandFilename,
                           TestFilename = resource.TestFilename,
                           TestPrinter = resource.TestPrinter
                       };
        }

        protected override void UpdateFromResource(LabelType entity, LabelTypeResource updateResource)
        {
            entity.LabelTypeCode = updateResource.LabelTypeCode;
            entity.Description = updateResource.Description;
            entity.BarcodePrefix = updateResource.BarcodePrefix;
            entity.NSBarcodePrefix = updateResource.NSBarcodePrefix;
            entity.Filename = updateResource.Filename;
            entity.DefaultPrinter = updateResource.DefaultPrinter;
            entity.CommandFilename = updateResource.CommandFilename;
            entity.TestFilename = updateResource.TestFilename;
            entity.TestPrinter = updateResource.TestPrinter;
            entity.TestCommandFilename = updateResource.TestCommandFilename;
        }

        protected override Expression<Func<LabelType, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
