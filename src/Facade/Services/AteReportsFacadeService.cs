namespace Linn.Production.Facade.Services
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Facade.Extensions;

    public class AteReportsFacadeService : IAteReportsFacadeService
    {
        private readonly IAteReportsService ateReportsService;

        public AteReportsFacadeService(IAteReportsService ateReportsService)
        {
            this.ateReportsService = ateReportsService;
        }

        public IResult<ResultsModel> GetStatusReport(
            string fromDate,
            string toDate,
            string smtOrPcb,
            string placeFound,
            string groupBy)
        {
            DateTime from;
            DateTime to;
            try
            {
                from = this.ConvertDate(fromDate);
                to = this.ConvertDate(toDate);
            }
            catch (InvalidDateException exception)
            {
                return new BadRequestResult<ResultsModel>(exception.Message);
            }

            return new SuccessResult<ResultsModel>(
                this.ateReportsService.GetStatusReport(from, to, smtOrPcb, placeFound, groupBy.ParseAteReportOption()));
        }

        public IResult<ResultsModel> GetDetailsReport(
            string fromDate,
            string toDate,
            string smtOrPcb,
            string placeFound,
            string board,
            string component,
            string faultCode)
        {
            DateTime from;
            DateTime to;
            try
            {
                from = this.ConvertDate(fromDate);
                to = this.ConvertDate(toDate);
            }
            catch (InvalidDateException exception)
            {
                return new BadRequestResult<ResultsModel>(exception.Message);
            }

            return new SuccessResult<ResultsModel>(
                this.ateReportsService.GetDetailsReport(
                    from,
                    to,
                    smtOrPcb,
                    placeFound,
                    board,
                    component,
                    faultCode));
        }

        private DateTime ConvertDate(string isoDate)
        {
            DateTime date;
            try
            {
                date = DateTime.Parse(isoDate);
            }
            catch (Exception)
            {
                throw new InvalidDateException("Invalid dates supplied to ate report");
            }

            return date;
        }
    }
}
