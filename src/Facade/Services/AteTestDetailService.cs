namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Resources;

    public class AteTestDetailService : FacadeService<AteTestDetail, AteTestDetailKey, AteTestDetailResource, AteTestDetailResource>
    {
        private readonly IRepository<Employee, int> employeeRepository;

        private readonly IRepository<AteTest, int> ateTestRepository;

        private readonly IRepository<PcasRevision, string> pcasRepository;

        public AteTestDetailService(
            IRepository<AteTestDetail, AteTestDetailKey> repository,
            IRepository<Employee, int> employeeRepository,
            IRepository<AteTest, int> ateTestRepository,
            IRepository<PcasRevision, string> pcasRepository,
            ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
            this.employeeRepository = employeeRepository;
            this.ateTestRepository = ateTestRepository;
            this.pcasRepository = pcasRepository;
        }

        protected override AteTestDetail CreateFromResource(AteTestDetailResource resource)
        {
            var existingDetails = this.ateTestRepository.FindById(resource.TestId)?.Details 
                                  != null && this.ateTestRepository.FindById(resource.TestId).Details.Any();
            return new AteTestDetail
                       {
                            TestId = resource.TestId,
                            ItemNumber = existingDetails ? this.ateTestRepository.FindById(resource.TestId).Details.Max(d => d.ItemNumber) + 1 : 1,
                            PartNumber = resource.PartNumber,
                            NumberOfFails = resource.NumberOfFails,
                            CircuitRef = resource.CircuitRef,
                            AteTestFaultCode = resource.AteTestFaultCode,
                            SmtOrPcb = resource.SmtOrPcb,
                            Shift = resource.Shift,
                            BatchNumber = resource.BatchNumber,
                            PcbOperator = this.employeeRepository.FilterBy(e => e.FullName == resource.PcbOperatorName)
                                .ToList().FirstOrDefault(),
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
            entity.PcbOperator = this.employeeRepository.FilterBy(e => e.FullName == resource.PcbOperatorName)
                .ToList().FirstOrDefault();
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