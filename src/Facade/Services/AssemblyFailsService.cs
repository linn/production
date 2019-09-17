﻿namespace Linn.Production.Facade.Services
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
                           CircuitPart = resource.CircuitPartNumber,
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

        protected override void UpdateFromResource(AssemblyFail assemblyFail, AssemblyFailResource resource)
        {
            assemblyFail.BoardPartNumber = resource.BoardPartNumber;
            assemblyFail.SerialNumber = resource.SerialNumber;
            assemblyFail.InSlot = resource.InSlot;
            assemblyFail.Machine = resource.Machine;
            assemblyFail.NumberOfFails = resource.NumberOfFails;
            assemblyFail.ReportedFault = resource.ReportedFault;
            assemblyFail.Analysis = resource.Analysis;
            assemblyFail.EngineeringComments = resource.EngineeringComments;
            assemblyFail.Shift = resource.Shift;
            assemblyFail.Batch = resource.Batch;
            assemblyFail.AoiEscape = resource.AoiEscape;
            assemblyFail.BoardPart = resource.BoardPartNumber != null
                                         ? this.partRepository.FindById(resource.BoardPartNumber)
                                         : null;
            assemblyFail.CircuitPart = resource.CircuitPartNumber;
            assemblyFail.CircuitPartRef = resource.CircuitRef;
            assemblyFail.CitResponsible = this.citRepository.FindById(resource.CitResponsible);
            assemblyFail.PersonResponsible = resource.PersonResponsible != null
                                                 ? this.employeeRepository.FindById((int)resource.PersonResponsible)
                                                 : null;
            assemblyFail.FaultCode = resource.FaultCode != null
                                        ? this.faultCodeRepository.FindById(resource.FaultCode)
                                        : null;
            assemblyFail.DateTimeComplete = resource.DateTimeComplete != null ? DateTime.Parse(resource.DateTimeComplete) : (DateTime?)null;
            assemblyFail.CompletedBy = resource.CompletedBy != null
                                           ? this.employeeRepository.FindById((int)resource.CompletedBy)
                                           : null;
            assemblyFail.CorrectiveAction = resource.CorrectiveAction;
            assemblyFail.OutSlot = resource.OutSlot;
            assemblyFail.CaDate = resource.CaDate != null ? DateTime.Parse(resource.CaDate) : (DateTime?)null;
            assemblyFail.DateInvalid = resource.DateInvalid != null ? DateTime.Parse(resource.DateInvalid) : (DateTime?)null;
        }

        protected override Expression<Func<AssemblyFail, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}