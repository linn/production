namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports;

    public class WhoBuiltWhatReportFacadeService : IWhoBuiltWhatReportFacadeService
    {
        private readonly IWhoBuiltWhatReport whoBuiltWhatReport;

        public WhoBuiltWhatReportFacadeService(IWhoBuiltWhatReport whoBuiltWhatReport)
        {
            this.whoBuiltWhatReport = whoBuiltWhatReport;
        }

        public IResult<IEnumerable<ResultsModel>> WhoBuiltWhat(string fromDate, string toDate, string citCode)
        {
            return new SuccessResult<IEnumerable<ResultsModel>>(this.whoBuiltWhatReport.WhoBuiltWhat(fromDate, toDate, citCode));
        }

        public IResult<ResultsModel> WhoBuiltWhatDetails(string fromDate, string toDate, int userNumber)
        {
            return new SuccessResult<ResultsModel>(this.whoBuiltWhatReport.WhoBuiltWhatDetails(fromDate, toDate, userNumber));
        }
    }
}
