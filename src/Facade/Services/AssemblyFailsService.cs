namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Proxy;

    using Linn.Production.Resources;

    public class AssemblyFailsService : FacadeService<AssemblyFail, int, AssemblyFailResource, AssemblyFailResource>
    {
        private readonly IDatabaseService databaseService;

        private readonly IRepository<Employee, int> employeeRepository;

        private readonly IRepository<AssemblyFailFaultCode, string> faultCodeRepository;

        private readonly IRepository<Cit, string> citRepository;

        private readonly IRepository<WorksOrder, int> worksOrderRepository;

        private readonly IRepository<Part, string> partRepository;

        public AssemblyFailsService(
            IRepository<AssemblyFail, int> assemblyFailRepository,
            IRepository<Employee, int> employeeRepository,
            IRepository<AssemblyFailFaultCode, string> faultCodeRepository,
            IRepository<WorksOrder, int> worksOrderRepository,
            IRepository<Cit, string> citRepository,
            IRepository<Part, string> partRepository,
            ITransactionManager transactionManager,
            IDatabaseService databaseService)
            : base(assemblyFailRepository, transactionManager)
        {
            this.databaseService = databaseService;
            this.employeeRepository = employeeRepository;
            this.faultCodeRepository = faultCodeRepository;
            this.citRepository = citRepository;
            this.worksOrderRepository = worksOrderRepository;
            this.partRepository = partRepository;
        }

        protected override AssemblyFail CreateFromResource(AssemblyFailResource resource)
        {
            return new AssemblyFail
                       {
                           Id = this.databaseService.GetNextVal("ASSEMBLY_FAULTS_SEQ"),
                           EnteredBy = this.employeeRepository.FindById(resource.EnteredBy),
                           NumberOfFails = resource.NumberOfFails,
                           InSlot = resource.InSlot,
                           DateTimeFound = DateTime.Parse(resource.DateTimeFound),
                           Machine = resource.Machine,
                           BoardSerial = resource.BoardSerial,
                           Shift = resource.Shift,
                           Batch = resource.Batch,
                           AoiEscape = resource.AoiEscape,
                           CircuitPartRef = resource.CircuitRef,
                           BoardPart = resource.BoardPartNumber != null ? this.partRepository.FindById(resource.BoardPartNumber) : null,
                           CitResponsible = resource.CitResponsible != null ? this.citRepository.FindById(resource.CitResponsible) : null,
                           BoardPartNumber = resource.BoardPartNumber,
                           PersonResponsible = resource.PersonResponsible != null
                                                   ? this.employeeRepository.FindById((int)resource.PersonResponsible)
                                                   : null,
                           CompletedBy = resource.CompletedBy != null 
                                             ? this.employeeRepository.FindById((int)resource.CompletedBy)
                                             : null,
                           ReturnedBy = resource.ReturnedBy != null
                                             ? this.employeeRepository.FindById((int)resource.ReturnedBy)
                                             : null,
                           DateInvalid = resource.DateInvalid != null ? DateTime.Parse(resource.DateInvalid) : (DateTime?)null,
                           CaDate = resource.CaDate != null ? DateTime.Parse(resource.CaDate) : (DateTime?)null,
                           SerialNumber = resource.SerialNumber,
                           WorksOrder = this.worksOrderRepository.FindById(resource.WorksOrderNumber),
                           ReportedFault = resource.ReportedFault,
                           OutSlot = resource.OutSlot,
                           CorrectiveAction = resource.CorrectiveAction,
                           FaultCode = resource.FaultCode != null 
                                           ? this.faultCodeRepository.FindById(resource.FaultCode)
                                           : null,
                           Analysis = resource.Analysis,
                           EngineeringComments = resource.EngineeringComments
                       };
        }

        protected override void UpdateFromResource(AssemblyFail entity, AssemblyFailResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<AssemblyFail, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}