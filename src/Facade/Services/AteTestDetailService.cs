namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    public class AteTestDetailService : FacadeService<AteTestDetail, AteTestDetailKey, AteTestDetailResource, AteTestDetailResource> 
    {
        public AteTestDetailService(IRepository<AteTestDetail, AteTestDetailKey> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override AteTestDetail CreateFromResource(AteTestDetailResource resource)
        {
            return new AteTestDetail
                       {
                            TestId = resource.TestId,
                            ItemNumber = resource.ItemNumber,
                            PartNumber = resource.PartNumber,
                            NumberOfFails = resource.NumberOfFails,
                            CircuitRef = resource.CircuitRef,
                            AteTestFaultCode = resource.AteTestFaultCode,
                            SmtOrPcb = resource.SmtOrPcb,
                            Shift = resource.Shift,
                            BatchNumber = resource.BatchNumber,
                            PcbOperator = resource.PcbOperator,
                            Comments = resource.Comments,
                            Machine = resource.Machine,
                            BoardFailNumber = resource.BoardFailNumber,
                            AoiEscape = resource.AoiEscape,
                            CorrectiveAction = resource.CorrectiveAction,
                            SmtFailId = resource.SmtFailId,
                            BoardSerialNumber = resource.BoardSerialNumber
                       };
        }

        protected override void UpdateFromResource(AteTestDetail entity, AteTestDetailResource resource)
        {
            entity.PartNumber = resource.PartNumber;
            entity.NumberOfFails = resource.NumberOfFails;
            entity.CircuitRef = resource.CircuitRef;
            entity.AteTestFaultCode = resource.AteTestFaultCode;
            entity.SmtOrPcb = resource.SmtOrPcb;
            entity.Shift = resource.Shift;
            entity.BatchNumber = resource.BatchNumber;
            entity.PcbOperator = resource.PcbOperator;
            entity.Comments = resource.Comments;
            entity.Machine = resource.Machine;
            entity.BoardFailNumber = resource.BoardFailNumber;
            entity.AoiEscape = resource.AoiEscape;
            entity.CorrectiveAction = resource.CorrectiveAction;
            entity.SmtFailId = resource.SmtFailId;
            entity.BoardSerialNumber = resource.BoardSerialNumber;
        }

        protected override Expression<Func<AteTestDetail, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}