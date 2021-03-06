﻿namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface ISmtReportsFacadeService
    {
        IResult<ResultsModel> GetPartsForOutstandingWorksOrders(string resourceSmtLine, string[] resourceParts);
    }
}