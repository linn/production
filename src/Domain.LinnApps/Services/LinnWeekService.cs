namespace Linn.Production.Domain.LinnApps.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        public LinnWeek GetWeek(DateTime date, IEnumerable<LinnWeek> weeks)
        {
            return weeks.FirstOrDefault(w => w.StartDate.Date <= date.Date && w.EndDate.Date >= date.Date);
        }

        public LinnWeek GetWeek(DateTime date)
        {
            return this.linnWeekRepository.GetWeek(date);
        }
    }
}
