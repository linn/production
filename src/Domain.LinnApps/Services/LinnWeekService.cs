namespace Linn.Production.Domain.LinnApps.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Production.Domain.LinnApps.Repositories;

    public class LinnWeekService : ILinnWeekService
    {
        private readonly ILinnWeekRepository linnWeekRepository;

        public LinnWeekService(ILinnWeekRepository linnWeekRepository)
        {
            this.linnWeekRepository = linnWeekRepository;
        }

        public IEnumerable<LinnWeek> GetWeeks(DateTime startDate, DateTime endDate)
        {
            return this.linnWeekRepository.GetWeeks(startDate, endDate);
        }

        public LinnWeek GetWeek(DateTime date)
        {
            return this.linnWeekRepository.GetWeek(date);
        }
    }
}
